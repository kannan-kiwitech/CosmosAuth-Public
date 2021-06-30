using CosmosAuth.Models;
using CosmosAuth.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Authentication;

namespace CosmosAuth.Services
{
    public class UserService : IUserService
    {
        private readonly IdentityContext _identityContext;
        private readonly ILogger _logger;
        private const string EMAIL_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

        public UserService(IdentityContext identityContext, ILogger<UserService> logger)
        {
            _identityContext = identityContext;
            _logger = logger;
        }

        public SimpleUser GetUser(HttpContext httpContext)
        {
            var email = httpContext.User.Claims.FirstOrDefault(c => c.Type == EMAIL_CLAIM)?.Value;
            if (email != null)
            {
                var user = _identityContext.Identities.FirstOrDefault(a => a.Email == email);
                return new SimpleUser
                {
                    FullName = user.FullName,
                    UserName = user.Email
                };
            }
            else
            {
                _logger.LogError("Invalid Access Token");
                throw new AuthenticationException("Invalid Access Token");
            }
        }
    }
}