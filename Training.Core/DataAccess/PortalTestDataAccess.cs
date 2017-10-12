using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Ninject;
using Training.Core.Util;

namespace Training.Core.DataAccess
{
    public class PortalTestDataAccess:GenericDataAccess<kurdev_portal_test>
    {

        public PortalTestDataAccess():base(new OrganizationServiceContext(new OrganizationServiceFactory().Create()))
        {
        }

        public IEnumerable<kurdev_portal_test> GetEntityByLogin(string login)
        {
            return (from c in service.CreateQuery<kurdev_portal_test>()
                where c.kurdev_Login == login
                    select  c).GetUniqueRecordsById();
        }
    }
}
