using System.Linq;
using AM.Domain.AccountAgg;
using Framework.Application;
using Framework.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AM.Application.Contract.Account.Models;

namespace AM.Infrastructure.EFCore.Repository
{
    public class AccountRepository : BaseRepository<long, Account>, IAccountRepository
    {
        #region inj

        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public Account GetBy(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == userName);
        }

        public EditAccount GetAccountDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                MobileNum = x.MobileNum,
                RoleId = x.RoleId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> GetAccountsList()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
            }).ToList();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                MobileNum = x.MobileNum,
                ProfileImg = x.ProfileImg,
                CreationDate = x.CreationDate.ToFarsi(),
                Role = x.Role.Name,
                RoleId = x.RoleId,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.MobileNum))
                query = query.Where(x => x.MobileNum.Contains(searchModel.MobileNum));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}