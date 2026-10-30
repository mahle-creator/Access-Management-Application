namespace Access_Management_Application.Models
{
    public class AccessLog
    {
        public int AccessLogId { get; set; }

        public string UserName { get; set; } = "";

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Decision { get; set; } = "";
    }
}