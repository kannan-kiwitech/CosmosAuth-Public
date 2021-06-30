using CosmosAuth.Models;
using CosmosAuth.Models.Dtos;
using CosmosAuth.Services;
using CosmosAuth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace CosmosAuth.Tests.Services
{
    internal class AuthServiceFake : IAuthService
    {
        private readonly List<Identity> _users;

        public AuthServiceFake()
        {
            _users = new List<Identity>
            {
                new Identity
                {
                    Email="testuser1@yopmail.com",
                    FullName="Test User1",
                    Id=Guid.NewGuid().ToString(),
                    Password=BCrypt.Net.BCrypt.HashPassword("P@ssword1"),
                    CreatedOn=DateTime.UtcNow
                },
                new Identity
                {
                    Email="testuser2@yopmail.com",
                    FullName="Test User2",
                    Id=Guid.NewGuid().ToString(),
                    Password=BCrypt.Net.BCrypt.HashPassword("P@ssword2"),
                    CreatedOn=DateTime.UtcNow
                }
            };
        }

        public SigninResponse Signin(SigninRequest loginRequest)
        {
            var user = _users.FirstOrDefault(a => a.Email == loginRequest.UserName);
            if (user != null)
            {
                bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
                if (isCorrectPassword)
                {
                    var token = JwtToken.Create(user, "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING", "https://localhost:44355", "http://localhost:4200");
                    return new SigninResponse
                    {
                        AccessToken = token
                    };
                }
            }
            throw new AuthenticationException("Invalid username or password");
        }

        public Task<Identity> SignupAsync(SignupRequest signupRequest)
        {
            throw new NotImplementedException();
        }
    }
}