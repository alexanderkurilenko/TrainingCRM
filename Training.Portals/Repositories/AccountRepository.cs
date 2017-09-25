using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Training.Portals.Models;

namespace Training.Portals.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private IOrganizationService service;

        public AccountRepository(IOrganizationService serv)
        {
            this.service = serv;
        }

        public void Create(Account item)
        {
            Entity AccountEntity = new Entity("account");
            if (item.AccountID != Guid.Empty)
            {
                AccountEntity["accountid"] = item.AccountID;
            }
            AccountEntity["name"] = item.AccountName;
            AccountEntity["numberofemployees"] = item.NumberOfEmployees;
            AccountEntity["revenue"] = item.Revenue;
            AccountEntity["primarycontactid"] =
                new EntityReference { Id = item.PrimaryContact.Id, LogicalName = "account" };

            if (item.AccountID == Guid.Empty)
            {
                item.AccountID = service.Create(AccountEntity);
            }
            else
            {
                service.Update(AccountEntity);
            }
        }

        public void Delete(Guid id)
        {
            service.Delete("account", id);
        }

        public Account Get(Guid id)
        {
            Account accountModel = new Account();
            ColumnSet cols = new ColumnSet(new String[] { "name", "revenue", "numberofemployees", "primarycontactid" });
            Entity account = service.Retrieve("account", id, cols);
            accountModel.AccountID = id;
            accountModel.AccountName = account.Attributes["name"].ToString();
            accountModel.NumberOfEmployees = (int)account.Attributes["numberofemployees"];
            accountModel.RevenueValue = ((Money)account.Attributes["revenue"]).Value;
            accountModel.PrimaryContact = (EntityReference)account.Attributes["primarycontactid"];
            return accountModel;
        }

        public List<EntityReference> GetEntityReference()
        {
            try
            {
                List<EntityReference> info = new List<EntityReference>();
               
                QueryExpression query = new QueryExpression
                {
                    EntityName = "contact",
                    ColumnSet = new ColumnSet("contactid", "fullname")
                };
                EntityCollection PrimaryContact = service.RetrieveMultiple(query);
                if (PrimaryContact != null && PrimaryContact.Entities.Count > 0)
                {
                    Microsoft.Xrm.Sdk.EntityReference itm;
                    for (int i = 0; i < PrimaryContact.Entities.Count; i++)
                    {
                        itm = new EntityReference();
                        if (PrimaryContact[i].Id != null)
                            itm.Id = PrimaryContact[i].Id;
                        if (PrimaryContact[i].Contains("fullname") && PrimaryContact[i]["fullname"] != null)
                            itm.Name = PrimaryContact[i]["fullname"].ToString();
                        itm.LogicalName = "contact";
                        info.Add(itm);
                    }
                }
                return info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Account> RetreiveAll()
        {
           
            QueryExpression query = new QueryExpression
            {
                EntityName = "account",
                ColumnSet = new ColumnSet("accountid", "name", "revenue", "numberofemployees", "primarycontactid")
            };
            List<Account> info = new List<Account>();
            EntityCollection accountRecord = service.RetrieveMultiple(query);
            if (accountRecord != null && accountRecord.Entities.Count > 0)
            {
                Account accountModel;
                for (int i = 0; i < accountRecord.Entities.Count; i++)
                {
                    accountModel = new Account();
                    if (accountRecord[i].Contains("accountid") && accountRecord[i]["accountid"] != null)
                        accountModel.AccountID = (Guid)accountRecord[i]["accountid"];
                    if (accountRecord[i].Contains("name") && accountRecord[i]["name"] != null)
                        accountModel.AccountName = accountRecord[i]["name"].ToString();
                    if (accountRecord[i].Contains("revenue") && accountRecord[i]["revenue"] != null)
                        accountModel.RevenueValue = ((Money)accountRecord[i]["revenue"]).Value;
                    if (accountRecord[i].Contains("numberofemployees") && accountRecord[i]["numberofemployees"] != null)
                        accountModel.NumberOfEmployees = (int)accountRecord[i]["numberofemployees"];
                    if (accountRecord[i].Contains("primarycontactid") && accountRecord[i]["primarycontactid"] != null)
                        accountModel.PrimaryContactName = ((EntityReference)accountRecord[i]["primarycontactid"]).Name;
                    info.Add(accountModel);
                }
            }
            return info;
        }

        public void Update(Account item)
        {
            Entity AccountEntity = new Entity("account");
            if (item.AccountID != Guid.Empty)
            {
                AccountEntity["accountid"] = item.AccountID;
            }
            AccountEntity["name"] = item.AccountName;
            AccountEntity["numberofemployees"] = item.NumberOfEmployees;
            AccountEntity["revenue"] = item.Revenue;
            AccountEntity["primarycontactid"] =
                new EntityReference { Id = item.PrimaryContact.Id, LogicalName = "account" };

            if (item.AccountID == Guid.Empty)
            {
                item.AccountID = service.Create(AccountEntity);
            }
            else
            {
                service.Update(AccountEntity);
            }
        }
    }
}