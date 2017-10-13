using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Xrm.Sdk;
using Ninject;
using Training.Importer.ImportType;
using Training.Importer.Infrastructure;

namespace Training.Importer.DataProccesor
{
    public class RecordLockDataProcessorDecorator<TImportEntity, TCrmEntity> :
        ImportDataProcessor<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity, ILockable
        where TCrmEntity : Entity
    {
        private static readonly RecordLockDictionary recordLockDictionary
            = new RecordLockDictionary();

        private readonly ImportDataProcessor<TImportEntity, TCrmEntity> dataProcessor;

        private readonly ILog logger = LogManager.GetLogger(typeof(IImportDataProcessor));

        [Inject]
        public RecordLockDataProcessorDecorator()
        {
            this.dataProcessor = new PortalTestDataProcessor();
        }

        protected internal override TCrmEntity Import(TImportEntity entity)
        {
            var lockObject = recordLockDictionary.GetLock(entity.UniqueIdentifier);

            lock (lockObject)
            {
                try
                {
                    logger.InfoFormat("Entity {0} was locked using the key = {1}", entity.GetType(), entity.UniqueIdentifier);
                    return dataProcessor.Import(entity);
                }
                finally
                {
                    recordLockDictionary.ReleaseLock(entity.UniqueIdentifier);
                    logger.InfoFormat("Lock for entity {0} with key {1} was released", entity.GetType(), entity.UniqueIdentifier);
                }
            }
        }
    }
}
