using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.Infrastructure
{
    public interface IImportFileSystem
    {
        IEnumerable<string> GetFiles();
        Stream GetFile(string filePath);
        void MoveToFailed(string filePath);
        void MoveToSucceed(string filePath);
    }
}
