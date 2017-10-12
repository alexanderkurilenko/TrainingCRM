using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Training.Importer.ImportType
{
    public class ImportEntity
    {
        public virtual Entity ParentCrmEntity { get; set; }

        public virtual string MainFieldsMessage
        {
            get { return string.Empty; }
        }
    }
}
