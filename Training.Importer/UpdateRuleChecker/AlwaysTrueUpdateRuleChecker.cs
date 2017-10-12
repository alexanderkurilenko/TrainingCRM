using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;

namespace Training.Importer.UpdateRuleChecker
{
    public class AlwaysTrueUpdateRuleChecker<TImportEntity, TCrmEntity> : IUpdateRuleChecker<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
    {
        public bool EntityHaveToBeUpdated(TImportEntity importEntity, TCrmEntity crmEntity)
        {
            return true;
        }
    }
}
