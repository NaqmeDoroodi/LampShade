using System.Collections.Generic;

namespace Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        string AccountRole();
        long AccountId();
        string AccountMobile();
        List<int> AccountPermissions();
        AuthViewModel AccountInfo();
        void Signin(AuthViewModel account);
    }
}
