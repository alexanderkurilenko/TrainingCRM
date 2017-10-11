using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class GZipArchiveReader : IArchiveReader
    {
        private readonly GZipStream gZipArchive;
        private readonly string archiveFileName;

        public GZipArchiveReader(Stream stream, string archiveFileName)
        {
            this.gZipArchive = new GZipStream(stream, CompressionMode.Decompress);
            this.archiveFileName = archiveFileName;
        }

        public IEnumerable<IArchiveEntry> GetTopLevelEntries()
        {
            return new List<GZipArchiveEntry> { new GZipArchiveEntry(gZipArchive, archiveFileName) };
        }

        public void Dispose()
        {
            gZipArchive.Dispose();
        }
    }
}
