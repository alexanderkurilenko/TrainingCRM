using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;


namespace Training.Portals.Utils
{
    public static class CRMHelper
    {
        public static OrganizationService GetCrmService()
        {
            CrmConnection crmConnection=new CrmConnection("CRM");
            return new OrganizationService(crmConnection);
        }
    }
}