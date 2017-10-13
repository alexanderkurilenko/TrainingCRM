using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.Deserializer
{
    public interface IImportDeserializerFactory
    {
        IImportDeserializer GetDeserializer(Stream stream);
    }
}
