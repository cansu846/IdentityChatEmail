namespace IdentityChatEmail.Models
{
    public class MessageDetailViewModel
    {
        public int MessageId { get; set; }
        public string SenderEmail { get; set; }
        public string RecieverEmail { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }
    }
}
