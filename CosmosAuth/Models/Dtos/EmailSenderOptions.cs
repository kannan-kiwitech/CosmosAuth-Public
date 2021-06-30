namespace CosmosAuth.Models.Dtos
{
    public class EmailSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string SendGridFromEmail { get; set; }
    }
}