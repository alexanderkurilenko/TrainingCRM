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
        public IImportDataProcessor GetImportDataProcessor(ImportEntity entity)
        {
            var entityType = entity.GetType();
            Console.WriteLine(entityType);

            if (entityType == typeof(PortalTest))
            {
                Console.WriteLine("tut");
                return new RecordLockDataProcessorDecorator<PortalTest, kurdev_portal_test>();
            }
            return null;
        }
    }
}
