using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using log4net;

namespace Training.Core.Util
{
    public class SerializerUtil<T> where T : class
    {
        private readonly ILog logger = LogManager.GetLogger("SerializerUtil");

        public T Deserialize(Stream stream)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error occurrs during object deserialization. Error: {0}.",
                    ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return null;
            }
        }
    }
}
