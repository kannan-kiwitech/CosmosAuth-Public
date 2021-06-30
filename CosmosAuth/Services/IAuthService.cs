using CosmosAuth.Models;
using CosmosAuth.Models.Dtos;
using System.Threading.Tasks;

namespace CosmosAuth.Services
{
    public interface IAuthService
    {
        SigninResponse Signin(SigninRequest loginRequest);

        Task<Identity> SignupAsync(SignupRequest signupRequest);
    }
}