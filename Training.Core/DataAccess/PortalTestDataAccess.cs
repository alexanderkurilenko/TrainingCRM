using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Ninject;

namespace Training.Core.DataAccess
{
    public class PortalTestDataAccess:GenericDataAccess<kurdev_portal_test>
    {

        public PortalTestDataAccess():base(new OrganizationServiceContext(new OrganizationServiceFactory().Create()))
        {
        }
    }
}
