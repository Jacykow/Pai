namespace Pai.Services
{
    public class AuthMessageSenderOptions
    {
        public string MailServer { get; set; } = "smtp.gmail.com";
        public int MailPort { get; set; } = 587;
        public string SenderName { get; set; } = "PAI Administration";
        public string Sender { get; set; } = "paiuserpai@gmail.com";
        public string Password { get; set; } = "paiPAIpai";
    }
}
