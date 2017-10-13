using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer
{
    public class FileNameEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            var res = Path.GetFileName(x) == Path.GetFileName(y);
            return res;
        }

        public int GetHashCode(string obj)
        {
            var fileName = Path.GetFileName(obj);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return obj.GetHashCode();
            }

            var result = fileName.GetHashCode();
            return result;
        }
    }
}
