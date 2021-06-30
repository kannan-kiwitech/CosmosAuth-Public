using CosmosAuth.Models.Dtos;
using CosmosAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CosmosAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// API to login using email id and password
        /// </summary>
        /// <returns>Access Token(JWT)</returns>
        /// <param name="signinRequest">Signin Request</param>
        /// <response code="200">Status code</response>
        /// <response code="400">Status code</response>
        /// <response code="401">Status code</response>
        [Route("Signin")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Signin([FromBody] SigninRequest signinRequest)
        {
            try
            {
                var result = _authService.Signin(signinRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// API to register user using email id, password and name
        /// </summary>
        /// <param name="signupRequest">Signup Request</param>
        /// <returns>User object</returns>
        /// <response code="200">Status code</response>
        /// <response code="400">Status code</response>
        [Route("Signup")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SignupAsync([FromBody] SignupRequest signupRequest)
        {
            try
            {
                var user = await _authService.SignupAsync(signupRequest);
                return CreatedAtAction("Signup", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}