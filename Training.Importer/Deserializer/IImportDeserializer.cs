using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.ImportType;

namespace Training.Importer.Deserializer
{
    public interface IImportDeserializer
    {
        IEnumerable<ImportEntity> Deserialize(Stream stream);
        Type AcceptedClass { get; }
    }
}
