using CosmosAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CosmosAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// API to fetch logged in users basic user profile
        /// </summary>
        /// <returns>Basic user profile</returns>
        /// <response code="200">Status code</response>
        /// <response code="400">Status code</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var user = _userService.GetUser(HttpContext);
            return Ok(user);
        }
    }
}