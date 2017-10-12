using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.ImportType
{
    public interface IImportEntityCollection<TImportEntity>
        where TImportEntity : ImportEntity
    {
        IEnumerable<TImportEntity> Entities { get; }
    }
}
