using CosmosAuth.Models.Dtos;
using Microsoft.AspNetCore.Http;

namespace CosmosAuth.Services
{
    public interface IUserService
    {
        SimpleUser GetUser(HttpContext httpContext);
    }
}