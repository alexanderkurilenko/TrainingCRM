using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;

namespace Training.Importer.DataProccesor
{
    public interface IImportDataProcessor
    {
        Type AcceptedEntity { get; }
        Entity ProcessEntity(ImportEntity entity);
    }
}
