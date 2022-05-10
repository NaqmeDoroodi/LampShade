using System.Collections.Generic;
using AM.Application.Contract.Role;
using AM.Application.Contract.Role.Models;
using AM.Domain.RoleAgg;
using Framework.Application;

namespace AM.Application
{
    public class RoleApplication : IRoleApplication
    {
        #region inj

        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository repository)
        {
            _repository = repository;
        }

        #endregion


        public OperationResult Create(CreateRole role)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Name == role.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var permissions = new List<Permission>();
            role.Permissions.ForEach(code => permissions.Add(new Permission(code)));
            var newRole = new Role(role.Name, permissions);

            _repository.Add(newRole);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole role)
        {
            var operation = new OperationResult();
            var roleToEdit = _repository.Get(role.Id);

            if (roleToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.Name == role.Name && x.Id != roleToEdit.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var permissions = new List<Permission>();
            role.Permissions.ForEach(code => permissions.Add(new Permission(code)));
            roleToEdit.Edit(role.Name, permissions);

            _repository.Save();
            return operation.Succeeded();
        }

        public EditRole GetDetailsRole(int id)
        {
            return _repository.GetRoleDetails(id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _repository.GetAllRoles();
        }
    }
}