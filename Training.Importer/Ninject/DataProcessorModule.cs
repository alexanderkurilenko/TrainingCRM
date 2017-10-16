﻿using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.DataProccesor;
using Training.Importer.ImportType.Models;

namespace Training.Importer.Ninject
{
    public class DataProcessorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IImportDataProcessor>().To<PortalTestDataProcessor>();
            Bind<ImportDataProcessor<PortalTest, kurdev_portal_test>>().To<PortalTestDataProcessor>();
            Bind<ImportDataProcessor<PortalTest, kurdev_portal_test>>().To<PortalTestDataProcessor>()
                   .WhenInjectedExactlyInto<RecordLockDataProcessorDecorator<PortalTest, kurdev_portal_test>>();
        }
    }
}
