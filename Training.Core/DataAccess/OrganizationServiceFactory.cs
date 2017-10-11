using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Training.Core.DataAccess
{
    public class OrganizationServiceFactory
    {
        public IOrganizationService Create()
        {

            CrmServiceClient conn = new CrmServiceClient(CrmConnectionConfig.GetServiceConfiguration());
            return conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
        }
    }
}
