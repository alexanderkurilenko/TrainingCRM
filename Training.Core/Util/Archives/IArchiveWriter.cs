using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public interface IArchiveWriter : IDisposable
    {
        void AddToArchive(Stream inputStream, string entryName);
    }
}
