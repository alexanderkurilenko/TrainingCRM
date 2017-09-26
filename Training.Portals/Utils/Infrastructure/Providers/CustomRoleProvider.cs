using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Training.Portals.Repositories;

namespace Training.Portals.Utils.Infrastructure.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private UnitOfWork unitOfWork;

        public CustomRoleProvider()
        {
            unitOfWork=new UnitOfWork();
            
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            var user = unitOfWork.Users.RetreiveAll().FirstOrDefault(usr => usr.Login == login);
            if (!ReferenceEquals(user,null) && user.Role.ToString() == roleName)
            {
                return true;
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles=new string[]{};
            var user = unitOfWork.Users.RetreiveAll().FirstOrDefault(u => u.Login == username);

            if (user == null) return roles;

            var role = user.Role.ToString();
            return new[] { role};

        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

    

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

      

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}