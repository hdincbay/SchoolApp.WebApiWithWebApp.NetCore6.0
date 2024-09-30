namespace SchoolApp.WebUI.Areas.Admin.ViewModels
{
    public class User
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }
    }
}
