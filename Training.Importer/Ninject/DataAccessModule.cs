using Microsoft.Xrm.Sdk.Client;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.DataAccess;

namespace Training.Importer.Ninject
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<PortalTestDataAccess>().ToSelf();
            Bind<OrganizationServiceFactory>().ToSelf();
            Bind<OrganizationServiceContext>().ToConstructor(context => new OrganizationServiceContext(new OrganizationServiceFactory().Create()));
        }
    }
}
