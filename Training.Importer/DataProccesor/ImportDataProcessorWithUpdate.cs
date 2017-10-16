using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;
using Training.Importer.RecordFinders;
using Training.Importer.UpdateRuleChecker;

namespace Training.Importer.DataProccesor
{
    public abstract class ImportDataProcessorWithUpdate<TImportEntity, TCrmEntity> :
        ImportDataProcessor<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
    {
        private readonly IUpdateRuleChecker<TImportEntity, TCrmEntity> updateRuleChecker;
        private readonly IExistingRecordFinder<TImportEntity, TCrmEntity> existingRecordFinder;
        private readonly ILog logger = LogManager.GetLogger(typeof(IImportDataProcessor));

        protected ImportDataProcessorWithUpdate(
            IExistingRecordFinder<TImportEntity, TCrmEntity> existingRecordFinder,
            IUpdateRuleChecker<TImportEntity, TCrmEntity> updateRuleChecker)
        {
            this.updateRuleChecker = updateRuleChecker;
            this.existingRecordFinder = existingRecordFinder;
        }

        protected internal override TCrmEntity Import(TImportEntity importEntity)
        {
          
            if (!PreImportCheck(importEntity))
            {
                
                return null;
            }

            var crmEntity = existingRecordFinder.FindExistingRecord(importEntity);
            
            if (crmEntity == null)
            {
                logger.InfoFormat("Existing record is not found. Creating the new record.");
                Console.WriteLine("Existing record is not found. Creating the new record.");
                crmEntity = CreateNew(importEntity);
            }
            else if (updateRuleChecker.EntityHaveToBeUpdated(importEntity, crmEntity))
            {
                logger.InfoFormat(
                    "Existing record was found. {0} record with Crm Id = {1} will be updated.",
                    crmEntity.LogicalName,
                    crmEntity.Id);
                Console.WriteLine("Existing record is  found. Updating the record.");
                crmEntity = UpdateExisting(crmEntity, importEntity);
            }

            return crmEntity;
        }

        protected abstract TCrmEntity CreateNew(TImportEntity importEntity);

        protected abstract TCrmEntity UpdateExisting(TCrmEntity crmEntity, TImportEntity importEntity);

        protected virtual bool PreImportCheck(TImportEntity importEntity)
        {
            return true;
        }
    }
}
