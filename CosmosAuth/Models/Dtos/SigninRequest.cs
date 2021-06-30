using System.ComponentModel.DataAnnotations;

namespace CosmosAuth.Models.Dtos
{
    public class SigninRequest
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Username should be a valid email address")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class SigninResponse
    {
        public string AccessToken { get; set; }
    }
}