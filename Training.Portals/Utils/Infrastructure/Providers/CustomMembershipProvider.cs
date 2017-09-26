using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using Training.Portals.Models;
using Training.Portals.Repositories;
using Roles = System.Web.Security.Roles;

namespace Training.Portals.Utils.Infrastructure.Providers
{
    public class CustomMembershipProvider:MembershipProvider
    {
        private UnitOfWork uniteOfWork;

        public CustomMembershipProvider()
        {
            uniteOfWork=new UnitOfWork();
        }


        public void CreateUser(string login, string password, string name, int role)
        {
            var user = new User
            {
                Login = login,
                Role = (Training.Portals.Models.Roles)role,
                Name = name,
                Password = password
            };
            uniteOfWork.Users.Create(user);

        }

        public override bool ValidateUser(string username, string password)
        {
            var user = uniteOfWork.Users.RetreiveAll().FirstOrDefault(usr => usr.Login == username);
            if (user != null && Crypto.VerifyHashedPassword(user.Password,password))
            {
                return true;
            }
            return false;
        }

        public MembershipUser CreateUser(string username, string password, string login)
        {
            var user = new User
            {
                Login = login,
                Role = Training.Portals.Models.Roles.User,
                Name = username,
                Password = password
            };
            uniteOfWork.Users.Create(user);
            var membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var user = uniteOfWork.Users.RetreiveAll().FirstOrDefault(usr => usr.Login == login);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, null, null, null,
                false, false, DateTime.Now,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);
            return memberUser;

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

     
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }


        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
    }
}