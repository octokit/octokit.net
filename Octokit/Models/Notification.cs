namespace Octokit
{
    public class Notification
    {
        public string Id { get; set; } // NB: API currently returns this as string which is Weird
        public Repository Repository { get; set; }
        public NotificationInfo Subject { get; set; }
        public string Reason { get; set; }
        public bool Unread { get; set; }
        public string UpdatedAt { get; set; }
        public string LastReadAt { get; set; }
        public string Url { get; set; }
    }
}
