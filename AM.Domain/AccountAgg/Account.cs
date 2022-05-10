using AM.Domain.RoleAgg;
using Framework.Domain;

namespace AM.Domain.AccountAgg
{
    public class Account : BaseEntity
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ProfileImg { get; private set; }
        public string MobileNum { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }


        public Account(string fullname, string username, string password, string profileImg, string mobileNum,
            int roleId)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            ProfileImg = profileImg;
            MobileNum = mobileNum;
            RoleId = roleId == 0 ? 3 : roleId;
        }
        

        public void Edit(string fullname, string username, string profileImg, string mobileNum, int roleId)
        {
            Fullname = fullname;
            Username = username;
            if (!string.IsNullOrWhiteSpace(profileImg)) ProfileImg = profileImg;
            MobileNum = mobileNum;
            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}