using System.Collections.Generic;
using System.Linq;
using AM.Application.Contract.Account;
using AM.Application.Contract.Account.Models;
using AM.Domain.AccountAgg;
using AM.Domain.RoleAgg;
using Framework.Application;

namespace AM.Application
{
    public class AccountApplication : IAccountApplication
    {
        #region inj

        private readonly IAccountRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository repository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        #endregion

        public OperationResult Register(RegisterAccount account)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Username == account.Username || x.MobileNum == account.MobileNum))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var filePath = account.ProfileImg == null
                ? _fileUploader.UploadDefaultUser()
                : _fileUploader.Upload(account.ProfileImg, "ProfileImages");
            var hashedPassword = _passwordHasher.Hash(account.Password);
            var newAccount = new Account(account.Fullname, account.Username, hashedPassword, filePath,
                account.MobileNum, account.RoleId);

            _repository.Add(newAccount);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount account)
        {
            var operation = new OperationResult();
            var accountToEdit = _repository.Get(account.Id);

            if (accountToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x =>
                    (x.Username == account.Username || x.MobileNum == account.MobileNum) && x.Id != account.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var filePath = _fileUploader.Upload(account.ProfileImg, "ProfileImages");
            accountToEdit.Edit(account.Fullname, account.Username, filePath, account.MobileNum, account.RoleId);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword account)
        {
            var operation = new OperationResult();
            var accountToEdit = _repository.Get(account.Id);

            if (accountToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (account.Password != account.RePassword)
                return operation.Failed(ApplicationMessage.PasswordNotMatch);

            var hashedPassword = _passwordHasher.Hash(account.Password);
            accountToEdit.ChangePassword(hashedPassword);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Login(Login accountToLogin)
        {
            var operation = new OperationResult();

            var account = _repository.GetBy(accountToLogin.UserName);

            if (account == null)
                return operation.Failed(ApplicationMessage.WrongUsernamePassword);

            (bool Verified, bool NeedsUpgrade)
                result = _passwordHasher.Check(account.Password, accountToLogin.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessage.WrongUsernamePassword);

            var permissions = _roleRepository.Get(account.RoleId).Permissions.Select(x => x.Code).ToList();
            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username, account.MobileNum, account.ProfileImg, permissions);

            _authHelper.Signin(authViewModel);
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public EditAccount GetAccountDetails(long id)
        {
            return _repository.GetAccountDetails(id);
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _repository.Get(id);
            return new AccountViewModel
            {
                Fullname = account.Fullname,
                MobileNum = account.MobileNum,
            };
        }

        public List<AccountViewModel> GetAccountsList()
        {
            return _repository.GetAccountsList();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}