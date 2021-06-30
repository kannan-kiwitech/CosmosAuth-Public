using CosmosAuth.Controllers;
using CosmosAuth.Models.Dtos;
using CosmosAuth.Services;
using CosmosAuth.Tests.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CosmosAuth.Tests.Tests
{
    public class AuthenticationControllerTest
    {
        private readonly AuthenticationController _authenticationController;
        private readonly IAuthService _authService;

        public AuthenticationControllerTest()
        {
            _authService = new AuthServiceFake();
            _authenticationController = new AuthenticationController(_authService);
        }

        [Fact]
        public void Signin_WhenCalled_WithCorrectCredentials_ReturnsOkResult()
        {
            // Correct Credentials
            var signinRequest = new SigninRequest
            {
                UserName = "testuser1@yopmail.com",
                Password = "P@ssword1"
            };

            var result = _authenticationController.Signin(signinRequest);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Signin_WhenCalled_WithWrongPassword_ReturnsUnAuthorizedResult()
        {
            //Wrong password correct username
            var signinRequest = new SigninRequest
            {
                UserName = "testuser1@yopmail.com",
                Password = "P@ssword2"
            };

            var result = _authenticationController.Signin(signinRequest);
            var unAuthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unAuthorizedResult);
            Assert.Equal(401, unAuthorizedResult.StatusCode);
        }

        [Fact]
        public void Signin_WhenCalled_WithWrongUsername_ReturnsUnAuthorizedResult()
        {
            //Wrong username
            var signinRequest = new SigninRequest
            {
                UserName = "testuser3@yopmail.com",
                Password = "P@ssword2"
            };

            var result = _authenticationController.Signin(signinRequest);
            var unAuthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unAuthorizedResult);
            Assert.Equal(401, unAuthorizedResult.StatusCode);
        }
    }
}