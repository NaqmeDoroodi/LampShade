using System.Collections.Generic;
using AM.Application.Contract.Account.Models;
using Framework.Domain;

namespace AM.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        Account GetBy(string userName);
        EditAccount GetAccountDetails(long id);
        List<AccountViewModel> GetAccountsList();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}