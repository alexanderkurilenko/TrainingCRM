using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.Deserializer
{
    public class DeserializerNotFoundException : Exception
    {
        public DeserializerNotFoundException(string entityName)
            : base(string.Format("Initializer not found for entity of {0} type", entityName))
        {
        }
    }
}
