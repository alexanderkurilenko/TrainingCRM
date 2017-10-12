using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.ImportType
{
    public interface ILegacyInformationComprisingImport
    {
        string LocalGUID { get; set; }
        string CreatedDate { get; set; }
        string CreatedTime { get; set; }
        string CreatedBy { get; set; }
        string LastModDate { get; set; }
        string LastModTime { get; set; }
        string LastModBy { get; set; }
        string System { get; set; }
        string EntityName { get; }
    }
}
