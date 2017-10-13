using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer
{
    public class ImportFolderConfiguration
    {
        public string ZippedFolder { get; set; }
        public string InitialFolder { get; set; }
        public string ProcessingFolder { get; set; }
        public string SucceedFolder { get; set; }
        public string FailedFolder { get; set; }
    }
}
