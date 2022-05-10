using System.Collections.Generic;
using AM.Application.Contract.Account.Models;
using Framework.Application;

namespace AM.Application.Contract.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount account);
        OperationResult Edit(EditAccount account);
        OperationResult ChangePassword(ChangePassword account);
        OperationResult Login(Login accountToLogin);
        void Logout();
        EditAccount GetAccountDetails(long id);
        AccountViewModel GetAccountBy(long id);
        List<AccountViewModel> GetAccountsList();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
