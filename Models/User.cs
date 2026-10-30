namespace Access_Management_Application.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; } = "";

        public string FacialData { get; set; } = "";

        public string FaceId { get; set; } = "";

        public string Status { get; set; } = "Active";
    }
}