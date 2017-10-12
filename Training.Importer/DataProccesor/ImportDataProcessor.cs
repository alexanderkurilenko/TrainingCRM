using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;

namespace Training.Importer.DataProccesor
{
    public abstract class ImportDataProcessor<TImportEntity, TCrmEntity> : IImportDataProcessor
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(IImportDataProcessor));

        public Type AcceptedEntity
        {
            get { return typeof(TImportEntity); }
        }

        // TODO: check another solution
        public Entity ProcessEntity(ImportEntity entity)
        {
            logger.InfoFormat("Import of {0} record with {1} is started", entity.GetType().Name, entity.MainFieldsMessage);
            var result = Import((TImportEntity)entity);
            logger.InfoFormat("Import of {0} record with {1} is finished", entity.GetType().Name, entity.MainFieldsMessage);
            return result;
        }

        protected internal abstract TCrmEntity Import(TImportEntity entity);
    }
}
