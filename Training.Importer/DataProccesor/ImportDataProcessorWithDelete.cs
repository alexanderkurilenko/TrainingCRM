using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;
using Training.Importer.RecordFinders;

namespace Training.Importer.DataProccesor
{
    public abstract class ImportDataProcessorWithDelete<TImportEntity, TCrmEntity, TCrmParentEntity>
        : ImportDataProcessor<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
        where TCrmParentEntity : Entity
    {
        private readonly IDeletionRecordFinder<TCrmParentEntity, TCrmEntity> deletionRecordFinder;

        private readonly ILog logger = LogManager.GetLogger(typeof(IImportDataProcessor));

        protected ImportDataProcessorWithDelete(IDeletionRecordFinder<TCrmParentEntity, TCrmEntity> deletionRecordFinder)
        {
            this.deletionRecordFinder = deletionRecordFinder;
        }

        public virtual void DeleteEntities(TCrmParentEntity parent)
        {
            var entities = deletionRecordFinder.FindRecordsForDeletion(parent).ToList();
            if (entities.Any())
            {
                logger.InfoFormat("Deleting all related {0} records for {1} parent entity", entities[0].LogicalName, parent.LogicalName);
            }

            foreach (var entity in entities)
            {
                DeleteEntity(entity, parent);
            }
        }

        protected abstract void DeleteEntity(TCrmEntity childEntity, TCrmParentEntity parent);
    }
}
