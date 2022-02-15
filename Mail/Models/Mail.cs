namespace Mail.Models
{
    public class Mail
    {
       public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
    }
}
