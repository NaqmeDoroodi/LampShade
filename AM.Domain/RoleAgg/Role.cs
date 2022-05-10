using System;
using System.Collections.Generic;
using AM.Domain.AccountAgg;

namespace AM.Domain.RoleAgg
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }


        public Role()
        {
            Accounts = new List<Account>();
            Permissions = new List<Permission>();
        }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            CreationDate = DateTime.Now;
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
