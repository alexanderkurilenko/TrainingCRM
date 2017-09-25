using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Training.Portals.Models;

namespace Training.Portals.Utils
{
    public static class DataAccess
    {
        public static List<PortalUser> RetreiveRecords()
        {
            CrmServiceClient conn = new Microsoft.Xrm.Tooling.Connector.CrmServiceClient(connectionString);
            using (OrganizationService service = new OrganizationService("CRM"))
            {
                QueryExpression query = new QueryExpression
                {
                    EntityName = "portal_test",
                    ColumnSet = new ColumnSet("kur_dev_portal_testid", "kur_dev_name", "kur_dev_login", "kur_dev_password")
                };
                List<PortalUser> info = new List<PortalUser>();
                EntityCollection accountRecord = service.RetrieveMultiple(query);
                if (accountRecord != null && accountRecord.Entities.Count > 0)
                {
                    PortalUser portalUserModel;
                    for (int i = 0; i < accountRecord.Entities.Count; i++)
                    {
                        portalUserModel = new PortalUser();

                        if (accountRecord[i].Contains("kur_dev_portal_testid") && accountRecord[i]["kur_dev_portal_testid"] != null)
                            portalUserModel.PortalUSerId = (Guid)accountRecord[i]["kur_dev_portal_testid"];

                        if (accountRecord[i].Contains("kur_dev_name") && accountRecord[i]["kur_dev_name"] != null)
                            portalUserModel.Name = accountRecord[i]["kur_dev_name"].ToString();

                        if (accountRecord[i].Contains("kur_dev_login") && accountRecord[i]["kur_dev_login"] != null)
                            portalUserModel.Login= accountRecord[i]["kur_dev_login"].ToString();

                        if (accountRecord[i].Contains("kur_dev_password") && accountRecord[i]["kur_dev_password"] != null)
                            portalUserModel.PassWord = accountRecord[i]["kur_dev_password"].ToString();
                       
                        info.Add(portalUserModel);
                    }
                }
                return info;
            }
        }

    }
        
    }
