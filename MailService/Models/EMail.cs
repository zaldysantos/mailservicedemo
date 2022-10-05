namespace MailService.Models
{
    public class EMail
    {
        public string? Subject { get; set; }

        public string? To { get; set; }

        public string? Body { get; set; }

        public bool IsSent { get; set; }
    }
}