using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Training.Portals.Models;

namespace Training.Portals.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private IOrganizationService service;

        public UserRepository(IOrganizationService serv)
        {
            this.service = serv;
        }

        //QueryExpression query = new QueryExpression
        //{
        //    EntityName = "kurdev_portal_test",
        //    ColumnSet = new ColumnSet("kurdev_portal_testid", "kurdev_login", "kurdev_password", "kurdev_name","kurdev_role")
        //};
        public void Create(User item)
        {
            Entity UserEntity = new Entity("kurdev_portal_test");
            if (item.UserId != Guid.Empty)
            {
                UserEntity["kurdev_portal_testid"] = item.UserId;
            }
            UserEntity["kurdev_name"] = item.Name;
            UserEntity["kurdev_login"] = item.Login;
            UserEntity["kurdev_password"] = item.Password;
            UserEntity["kurdev_role"] = new OptionSetValue((int)item.Role);

            if (item.UserId == Guid.Empty)
            {
                item.UserId= service.Create(UserEntity);
            }
            else
            {
                service.Update(UserEntity);
            }
        }

        public void Delete(Guid id)
        {
            service.Delete("kurdev_portal_test", id);
        }

        public User Get(Guid id)
        {
            User userModel = new User();
            ColumnSet cols = new ColumnSet(new String[] {"kurdev_login", "kurdev_password", "kurdev_name", "kurdev_role" });
            Entity user = service.Retrieve("kurdev_portal_test", id, cols);
            userModel.UserId = id;
            userModel.Login = user.Attributes["kurdev_login"].ToString();
            userModel.Password = user.Attributes["kurdev_password"].ToString();
            userModel.Name = user.Attributes["kurdev_name"].ToString();
            userModel.Role=((Roles)((OptionSetValue)user.Attributes["kurdev_role"]).Value); ;
            return userModel;
        }

        public List<EntityReference> GetEntityReference()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> RetreiveAll()
        {
            QueryExpression query = new QueryExpression
            {
                EntityName = "kurdev_portal_test",
                ColumnSet = new ColumnSet("kurdev_portal_testid", "kurdev_login", "kurdev_password", "kurdev_name","kurdev_role")
            };
            List<User> info = new List<User>();
            EntityCollection accountRecord =service.RetrieveMultiple(query);
            if (accountRecord != null && accountRecord.Entities.Count > 0)
            {
                User userModel;
                for (int i = 0; i < accountRecord.Entities.Count; i++)
                {
                    userModel = new User();
                    if (accountRecord[i].Contains("kurdev_portal_testid") && accountRecord[i]["kurdev_portal_testid"] != null)
                        userModel.UserId = (Guid)accountRecord[i]["kurdev_portal_testid"];

                    if (accountRecord[i].Contains("kurdev_login") && accountRecord[i]["kurdev_login"] != null)
                        userModel.Login = accountRecord[i]["kurdev_login"].ToString();

                    if (accountRecord[i].Contains("kurdev_password") && accountRecord[i]["kurdev_password"] != null)
                        userModel.Password = accountRecord[i]["kurdev_password"].ToString();

                    if (accountRecord[i].Contains("kurdev_name") && accountRecord[i]["kurdev_name"] != null)
                        userModel.Name = accountRecord[i]["kurdev_name"].ToString();

                    if (accountRecord[i].Contains("kurdev_role") && accountRecord[i]["kurdev_role"] != null)
                        userModel.Role = ((Roles)((OptionSetValue)accountRecord[i]["kurdev_role"]).Value);

                    info.Add(userModel);
                }
            }
            return info;
        }

        public void Update(User item)
        {
            Entity UserEntity = new Entity("kurdev_portal_test");
            if (item.UserId != Guid.Empty)
            {
                UserEntity["kurdev_portal_testid"] = item.UserId;
            }
            UserEntity["kurdev_name"] = item.Name;
            UserEntity["kurdev_login"] = item.Login;
            UserEntity["kurdev_password"] = item.Password;
            UserEntity["kurdev_role"] = new OptionSetValue((int)item.Role);

            if (item.UserId == Guid.Empty)
            {
                item.UserId = service.Create(UserEntity);
            }
            else
            {
                service.Update(UserEntity);
            }
        }
    }
}