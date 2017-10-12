using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Core.DataAccess;
using Training.Importer.ImportType.Models;

namespace Training.Importer.RecordFinders
{
    public class PortalTestRecordFinder : IExistingRecordFinder<PortalTest, kurdev_portal_test>,IDisposable
    {
        protected readonly PortalTestDataAccess portalTestDataAccess;

        public PortalTestRecordFinder()
        {
            this.portalTestDataAccess =new PortalTestDataAccess();
        }

        public void Dispose()
        {
            portalTestDataAccess.Dispose();
        }

        public virtual kurdev_portal_test FindExistingRecord(PortalTest importEntity)
        {
            var res =portalTestDataAccess.GetEntityByLogin(importEntity.Login).FirstOrDefault();
            portalTestDataAccess.Detach(res);
            return res;

        }
    }
}
