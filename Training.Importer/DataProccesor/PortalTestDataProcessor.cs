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
            SetData(ref crmEntity, importEntity);
            portalTestDataAccess.Detach(crmEntity);
            portalTestDataAccess.Update(crmEntity);
            return crmEntity;
        }

        private void SetData(  ref kurdev_portal_test entity,PortalTest portalTest)
        {
            Console.WriteLine(portalTest.UserId);
            Mapper.Initialize(cfg => cfg.CreateMap<PortalTest, kurdev_portal_test>()
            .ForMember("kurdev_Login", opt => opt.MapFrom(c => c.Login))
            .ForMember("kurdev_name", opt => opt.MapFrom(c => c.Name))
            .ForMember("kurdev_PassWord", opt => opt.MapFrom(c => c.Password))
            .ForMember("kurdev_Role", opt => opt.MapFrom(c => new OptionSetValue((int)c.Role)))
            .ForMember("kurdev_portal_testId", opt => opt.MapFrom(c => c.UserId==Guid.Empty?Guid.NewGuid():c.UserId)));
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
