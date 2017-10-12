using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.ImportType;

namespace Training.Importer.DataProccesor
{
    public interface IImportDataProcessorFactory
    {
        IImportDataProcessor GetImportDataProcessor(ImportEntity entity);
    }
}
