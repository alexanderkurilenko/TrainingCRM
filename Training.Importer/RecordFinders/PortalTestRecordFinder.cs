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

        public PortalTestRecordFinder(PortalTestDataAccess _portalTestDataAccess)
        {
            this.portalTestDataAccess =_portalTestDataAccess;
        }

        public void Dispose()
        {
            portalTestDataAccess.Dispose();
        }

        public virtual kurdev_portal_test FindExistingRecord(PortalTest importEntity)
        {

            if (importEntity.UserId != Guid.Empty)
            {
                var res = portalTestDataAccess.GetById(importEntity.UserId);
                if (res != null)
                    portalTestDataAccess.Detach(res);
                return res;
            }
            var result = portalTestDataAccess.GetEntityByLogin(importEntity.Login).FirstOrDefault();
                if (result != null)
                    portalTestDataAccess.Detach(result);
                return result;
           
          

        }
    }
}
