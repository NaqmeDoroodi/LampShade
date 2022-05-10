using System.Collections.Generic;

namespace Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string ProfileImg { get; set; }
        public List<int> Permissions { get; set; }

        public AuthViewModel()
        {
        }

        public AuthViewModel(long id, long roleId, string fullname, string username, string mobile, string profileImg, List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            ProfileImg = profileImg;
            Permissions = permissions;
        }
    }
}