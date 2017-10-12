using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Training.Importer.RecordFinders
{
    public interface IDeletionRecordFinder<TCrmParentEntity, TCrmChildEntity>
        where TCrmParentEntity : Entity
        where TCrmChildEntity : Entity
    {
        IEnumerable<TCrmChildEntity> FindRecordsForDeletion(TCrmParentEntity parentEntity);
    }
}
