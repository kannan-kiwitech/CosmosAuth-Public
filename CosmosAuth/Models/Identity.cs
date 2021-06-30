using System;

namespace CosmosAuth.Models
{
    public class Identity
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}