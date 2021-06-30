using CosmosAuth.Models;
using CosmosAuth.Models.Dtos;
using CosmosAuth.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace CosmosAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IdentityContext _identityContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public AuthService(IdentityContext identityContext, IConfiguration configuration, ILogger<AuthService> logger, IWebHostEnvironment env, IEmailService emailService)
        {
            _identityContext = identityContext;
            _configuration = configuration;
            _logger = logger;
            _env = env;
            _emailService = emailService;
        }

        public SigninResponse Signin(SigninRequest loginRequest)
        {
            try
            {
                //Check the user exists or not
                var user = _identityContext.Identities.FirstOrDefault(a => a.Email == loginRequest.UserName);
                if (user != null)
                {
                    //Validate the credentials
                    bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
                    if (isCorrectPassword)
                    {
                        //Create JWT
                        var token = JwtToken.Create(user, _configuration["JWT:Secret"], _configuration["JWT:ValidIssuer"], _configuration["JWT:ValidAudience"]);

                        _logger.LogInformation($"Token created successfully for {user.Id}");
                        return new SigninResponse
                        {
                            AccessToken = token
                        };
                    }
                }
                //Send a generic message to client
                throw new AuthenticationException("Invalid username or password");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private BodyBuilder GetEmailContent(string templateName)
        {
            var pathToFile = $"{_env.ContentRootPath}{Path.DirectorySeparatorChar}Templates{Path.DirectorySeparatorChar}{templateName}";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            return builder;
        }

        public async Task<Identity> SignupAsync(SignupRequest signupRequest)
        {
            try
            {
                var userExists = _identityContext.Identities.FirstOrDefault(a => a.Email == signupRequest.UserName);
                if (userExists != null)
                {
                    throw new AuthenticationException("User with same username/email already exists");
                }

                var user = new Identity
                {
                    CreatedOn = DateTime.UtcNow,
                    Email = signupRequest.UserName,
                    FullName = signupRequest.FullName,
                    Password = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password),
                    Id = Guid.NewGuid().ToString()
                };
                await _identityContext.AddAsync(user);

                var result = await _identityContext.SaveChangesAsync();
                if (result > 0)
                {
                    var messageBuilder = GetEmailContent("WelcomeEmail.html");
                    string messageBody = string.Format(messageBuilder.HtmlBody, user.FullName);
                    await _emailService.SendEmailAsync(user.Email, "Welcome to Bilbayt", messageBody);

                    _logger.LogInformation($"Signup completed successfully for {signupRequest.UserName}");
                    return user;
                }
                else
                    throw new AuthenticationException("Unable to signup the user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}