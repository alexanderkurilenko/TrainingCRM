using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Core.DataAccess;
using Training.Importer.ImportType.Models;
using Training.Importer.RecordFinders;
using Training.Importer.UpdateRuleChecker;

namespace Training.Importer.DataProccesor
{
    public class PortalTestDataProcessor : ImportDataProcessorWithUpdate<PortalTest,kurdev_portal_test>,IDisposable
    {
        private readonly PortalTestDataAccess portalTestDataAccess;

        public PortalTestDataProcessor():base(new PortalTestRecordFinder(), new AlwaysTrueUpdateRuleChecker<PortalTest,kurdev_portal_test>())
        {
            portalTestDataAccess=new PortalTestDataAccess();
        }

        protected override kurdev_portal_test CreateNew(PortalTest importEntity)
        {
            var pt=new kurdev_portal_test();
            Console.WriteLine(pt.Id);
            SetData(pt,importEntity);
            portalTestDataAccess.Create(pt);
            return pt;
        }

        protected override kurdev_portal_test UpdateExisting(kurdev_portal_test crmEntity, PortalTest importEntity)
        {
            SetData(crmEntity, importEntity);
            portalTestDataAccess.Detach(crmEntity);
            portalTestDataAccess.Update(crmEntity);
            return crmEntity;
        }

        private void SetData(kurdev_portal_test entity,PortalTest portalTest)
        {
            
            entity.kurdev_Login = portalTest.Login;
            entity.kurdev_Role = new OptionSetValue((int) portalTest.Role);
            entity.kurdev_name = portalTest.Name;
            entity.kurdev_PassWord = portalTest.Password;
        }

        public void Dispose()
        {
            portalTestDataAccess?.Dispose();
        }
    }
}
