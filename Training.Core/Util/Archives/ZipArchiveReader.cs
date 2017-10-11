using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class ZipArchiveReader : IArchiveReader
    {
        private readonly ZipArchive zipArchive;

        public ZipArchiveReader(Stream stream)
        {
            zipArchive = new ZipArchive(stream);
        }

        public IEnumerable<IArchiveEntry> GetTopLevelEntries()
        {
            return zipArchive.Entries.Select(entry => new ZipArchiveEntry(entry));
        }

        public void Dispose()
        {
            zipArchive.Dispose();
        }
    }
}
