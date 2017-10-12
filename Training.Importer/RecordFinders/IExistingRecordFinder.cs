using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;

namespace Training.Importer.RecordFinders
{

    public interface IExistingRecordFinder<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
    {
        TCrmEntity FindExistingRecord(TImportEntity importEntity);
    }
}
