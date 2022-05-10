namespace AM.Application.Contract.Account.Models
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string ProfileImg { get; set; }
        public string MobileNum { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string CreationDate { get; set; }
    }
}