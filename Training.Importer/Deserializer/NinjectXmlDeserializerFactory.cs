using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using Ninject;
using Training.Core.Util;

namespace Training.Importer.Deserializer
{

    public class NinjectXmlDeserializerFactory : IImportDeserializerFactory
    {
        private readonly Dictionary<string, IImportDeserializer> serializerMapping;
        private readonly ILog logger = LogManager.GetLogger(typeof(NinjectXmlDeserializerFactory));

        [Inject]
        public NinjectXmlDeserializerFactory(IEnumerable<IImportDeserializer> serializers)
        {
            serializerMapping = new Dictionary<string, IImportDeserializer>();
            serializers.ForEach(s => serializerMapping.Add(s.AcceptedClass.Name, s));
        }

        //TODO (STS): rewrite on XmlTextReader
        public IImportDeserializer GetDeserializer(Stream stream)
        {
            var doc = new XmlDocument();
            doc.Load(stream);

            if (doc.DocumentElement != null && serializerMapping.ContainsKey(doc.DocumentElement.FirstChild.Name))
            {
                return serializerMapping[doc.DocumentElement.FirstChild.Name];
            }

            //TODO (STS): expand exception message
            throw new DeserializerNotFoundException(doc.DocumentElement.FirstChild.Name);
        }
    }
}
