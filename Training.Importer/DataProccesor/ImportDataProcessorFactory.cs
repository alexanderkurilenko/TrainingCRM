using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.ImportType;
using Training.Importer.ImportType.Models;

namespace Training.Importer.DataProccesor
{
    public class ImportDataProcessorFactory : IImportDataProcessorFactory
    {
        private readonly IKernel kernel;

        [Inject]
        public ImportDataProcessorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IImportDataProcessor GetImportDataProcessor(ImportEntity entity)
        {

            var entityType = entity.GetType();
            Console.WriteLine(entityType);

            if (entityType == typeof(PortalTest))
            {
                
                return kernel.Get<RecordLockDataProcessorDecorator<PortalTest, kurdev_portal_test>>();
            }
            return null;
        }
    }
}
