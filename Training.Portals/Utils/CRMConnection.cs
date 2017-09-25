using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.ApplicationInsights.Web;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Training.Portals.Models;

namespace Training.Portals.Utils
{
    public class CRMConnection
    {
        private IOrganizationService _orgService;

        public List<AccountEntityModels> RetrieveEntities()
        {
            CrmServiceClient conn = new CrmServiceClient(GetServiceConfiguration());
            _orgService = conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            Guid userid = ((WhoAmIResponse)_orgService.Execute(new WhoAmIRequest())).UserId;
            QueryExpression query = new QueryExpression
            {
                EntityName = "account",
                ColumnSet = new ColumnSet("accountid", "name", "revenue", "numberofemployees", "primarycontactid")
            };
            List<AccountEntityModels> info = new List<AccountEntityModels>();
            EntityCollection accountRecord = _orgService.RetrieveMultiple(query);
            if (accountRecord != null && accountRecord.Entities.Count > 0)
            {
                AccountEntityModels accountModel;
                for (int i = 0; i < accountRecord.Entities.Count; i++)
                {
                    accountModel = new AccountEntityModels();
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

        public List<EntityReference> GetEntityReference()
        {
            try
            {
                List<EntityReference> info = new List<EntityReference>();
                CrmServiceClient conn = new CrmServiceClient(GetServiceConfiguration());
                _orgService = conn.OrganizationWebProxyClient != null
                    ? (IOrganizationService) conn.OrganizationWebProxyClient
                    : (IOrganizationService) conn.OrganizationServiceProxy;
                QueryExpression query = new QueryExpression
                {
                    EntityName = "contact",
                    ColumnSet = new ColumnSet("contactid", "fullname")
                };
                EntityCollection PrimaryContact = _orgService.RetrieveMultiple(query);
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

        public AccountEntityModels GetCurrentRecord(Guid accountId)
        {
            AccountEntityModels accountModel=new AccountEntityModels();
            CrmServiceClient conn = new CrmServiceClient(GetServiceConfiguration());
            _orgService = conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            ColumnSet cols = new ColumnSet(new String[] { "name", "revenue", "numberofemployees", "primarycontactid" });
            Entity account = _orgService.Retrieve("account", accountId, cols);
            accountModel.AccountID = accountId;
            accountModel.AccountName = account.Attributes["name"].ToString();
            accountModel.NumberOfEmployees = (int)account.Attributes["numberofemployees"];
            accountModel.RevenueValue = ((Money)account.Attributes["revenue"]).Value;
            accountModel.PrimaryContact = (EntityReference)account.Attributes["primarycontactid"];
            return accountModel;
        }

        public void SaveAccount(AccountEntityModels objAccountModel)
        {
            CrmServiceClient conn = new CrmServiceClient(GetServiceConfiguration());
            _orgService = conn.OrganizationWebProxyClient != null
                ? (IOrganizationService) conn.OrganizationWebProxyClient
                : (IOrganizationService) conn.OrganizationServiceProxy;
            Entity AccountEntity = new Entity("account");
            if (objAccountModel.AccountID != Guid.Empty)
            {
                AccountEntity["accountid"] = objAccountModel.AccountID;
            }
            AccountEntity["name"] = objAccountModel.AccountName;
            AccountEntity["numberofemployees"] = objAccountModel.NumberOfEmployees;
            AccountEntity["revenue"] = objAccountModel.Revenue;
            AccountEntity["primarycontactid"] =
                new Microsoft.Xrm.Sdk.EntityReference {Id = objAccountModel.PrimaryContact.Id, LogicalName = "account"};

            if (objAccountModel.AccountID == Guid.Empty)
            {
                objAccountModel.AccountID = _orgService.Create(AccountEntity);
            }
            else
            {
               _orgService.Update(AccountEntity);

            }
        }

        public void DeleteRecord(Guid id)
        {
            CrmServiceClient conn = new CrmServiceClient(GetServiceConfiguration());
            _orgService = conn.OrganizationWebProxyClient != null
                ? (IOrganizationService)conn.OrganizationWebProxyClient
                : (IOrganizationService)conn.OrganizationServiceProxy;

                _orgService.Delete("account", id);
  

        }
        #region Private Methods

        /// <summary>
        /// Gets web service connection information from the app.config file.
        /// If there is more than one available, the user is prompted to select
        /// the desired connection configuration by name.
        /// </summary>
        /// <returns>A string containing web service connection configuration information.</returns>
        private static String GetServiceConfiguration()
        {
            // Get available connection strings from app.config.
            int count = ConfigurationManager.ConnectionStrings.Count;

            // Create a filter list of connection strings so that we have a list of valid
            // connection strings for Microsoft Dynamics CRM only.
            List<KeyValuePair<String, String>> filteredConnectionStrings =
                new List<KeyValuePair<String, String>>();

            for (int a = 0; a < count; a++)
            {
                if (isValidConnectionString(ConfigurationManager.ConnectionStrings[a].ConnectionString))
                    filteredConnectionStrings.Add
                        (new KeyValuePair<string, string>
                            (ConfigurationManager.ConnectionStrings[a].Name,
                            ConfigurationManager.ConnectionStrings[a].ConnectionString));
            }

            // No valid connections strings found. Write out and error message.
            if (filteredConnectionStrings.Count == 0)
            {
                Console.WriteLine("An app.config file containing at least one valid Microsoft Dynamics CRM " +
                    "connection string configuration must exist in the run-time folder.");
                Console.WriteLine("\nThere are several commented out example connection strings in " +
                    "the provided app.config file. Uncomment one of them and modify the string according " +
                    "to your Microsoft Dynamics CRM installation. Then re-run the sample.");
                return null;
            }

            // If one valid connection string is found, use that.
            if (filteredConnectionStrings.Count == 1)
            {
                return filteredConnectionStrings[0].Value;
            }

            // If more than one valid connection string is found, let the user decide which to use.
            if (filteredConnectionStrings.Count > 1)
            {
                Console.WriteLine("The following connections are available:");
                Console.WriteLine("------------------------------------------------");

                for (int i = 0; i < filteredConnectionStrings.Count; i++)
                {
                    Console.Write("\n({0}) {1}\t",
                    i + 1, filteredConnectionStrings[i].Key);
                }

                Console.WriteLine();

                Console.Write("\nType the number of the connection to use (1-{0}) [{0}] : ",
                    filteredConnectionStrings.Count);
                String input = Console.ReadLine();
                int configNumber;
                if (input == String.Empty) input = filteredConnectionStrings.Count.ToString();
                if (!Int32.TryParse(input, out configNumber) || configNumber > count ||
                    configNumber == 0)
                {
                    Console.WriteLine("Option not valid.");
                    return null;
                }

                return filteredConnectionStrings[configNumber - 1].Value;

            }
            return null;

        }


        /// <summary>
        /// Verifies if a connection string is valid for Microsoft Dynamics CRM.
        /// </summary>
        /// <returns>True for a valid string, otherwise False.</returns>
        private static Boolean isValidConnectionString(String connectionString)
        {
            // At a minimum, a connection string must contain one of these arguments.
            if (connectionString.Contains("Url=") ||
                connectionString.Contains("Server=") ||
                connectionString.Contains("ServiceUri="))
                return true;

            return false;
        }

        #endregion Private Methods
    }
}