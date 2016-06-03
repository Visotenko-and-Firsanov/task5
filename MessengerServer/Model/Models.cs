namespace Model
{
    public class Profile
    {
        public long ProfileId { get; set; }
        public string ProfileName { get; set; }
        public bool Online { get; set; }
    }

    public class Contact
    {
        public long ProfileId { get; set; }
        public long UserId { get; set; }
        public long ContactId { get; set; }
    }

    public class Message
    {
        public long ProfileId { get; set; }
        public long UserId { get; set; }
        public long SenderId { get; set; }
        public string Messages { get; set; }
    }
}
