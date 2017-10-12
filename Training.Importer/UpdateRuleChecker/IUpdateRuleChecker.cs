using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Training.Importer.ImportType;

namespace Training.Importer.UpdateRuleChecker
{
    public interface IUpdateRuleChecker<TImportEntity, TCrmEntity>
        where TImportEntity : ImportEntity
        where TCrmEntity : Entity
    {
        bool EntityHaveToBeUpdated(TImportEntity importEntity, TCrmEntity crmEntity);
    }
}
