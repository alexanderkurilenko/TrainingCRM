using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;
using Training.Importer.ImportType;

namespace Training.Importer.Deserializer
{

    public class GenericXmlImportDeserializer<TImportEntityCollection, TImportEntity> : IImportDeserializer
        where TImportEntityCollection : IImportEntityCollection<TImportEntity>
        where TImportEntity : ImportEntity
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(IImportDeserializer));

        public IEnumerable<ImportEntity> Deserialize(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(TImportEntityCollection));

            var importEntityCollection = (TImportEntityCollection)serializer.Deserialize(stream);

            return importEntityCollection.Entities;
        }

        public Type AcceptedClass
        {
            get { return typeof(TImportEntity); }
        }
    }
}
