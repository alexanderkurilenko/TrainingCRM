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
using Training.Importer.Mappers;
using AutoMapper;

namespace Training.Importer.DataProccesor
{
    public class PortalTestDataProcessor : ImportDataProcessorWithUpdate<PortalTest,kurdev_portal_test>,IDisposable
    {
        private readonly PortalTestDataAccess portalTestDataAccess;

        public PortalTestDataProcessor(PortalTestDataAccess _portalTestDataAccess) :base(new PortalTestRecordFinder(_portalTestDataAccess), new AlwaysTrueUpdateRuleChecker<PortalTest,kurdev_portal_test>())
        {
            portalTestDataAccess=_portalTestDataAccess;
        }

        protected override kurdev_portal_test CreateNew(PortalTest importEntity)
        {
            var pt=new kurdev_portal_test();
            //Console.WriteLine(importEntity.Login);
            SetData( ref pt,importEntity);
            Console.WriteLine(pt.kurdev_Login);
            Console.WriteLine(pt.kurdev_PassWord);

            portalTestDataAccess.Create(pt);
            return pt;
        }

        protected override kurdev_portal_test UpdateExisting(kurdev_portal_test crmEntity, PortalTest importEntity)
        {
            SetDataForUpdate(ref crmEntity, importEntity,crmEntity.Id);
            portalTestDataAccess.Detach(crmEntity);
            portalTestDataAccess.Update(crmEntity);
            return crmEntity;
        }

        private void SetDataForUpdate(ref kurdev_portal_test crmEntity, PortalTest importEntity, Guid id)
        {
            var tmp = importEntity;
            if (importEntity.UserId == Guid.Empty)
            {
                tmp.UserId = crmEntity.Id;
            }
            Console.WriteLine(tmp.UserId);
            Console.WriteLine(tmp.Login);
            MapperBase.GetMappers();
            crmEntity = Mapper.Map<PortalTest, kurdev_portal_test>(tmp);
        }

        private void SetData(  ref kurdev_portal_test entity,PortalTest portalTest)
        {
            Console.WriteLine(portalTest.UserId);
            MapperBase.GetMappers();
            entity = Mapper.Map<PortalTest, kurdev_portal_test>(portalTest);
            //entity = d;
            //entity.kurdev_Login = portalTest.Login;
            //entity.kurdev_Role = new OptionSetValue((int)portalTest.Role);
            //entity.kurdev_name = portalTest.Name;
            //entity.kurdev_PassWord = portalTest.Password;

        }

        public void Dispose()
        {
            portalTestDataAccess?.Dispose();
        }
    }

   
}
