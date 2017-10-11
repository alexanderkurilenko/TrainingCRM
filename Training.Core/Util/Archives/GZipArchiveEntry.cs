using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class GZipArchiveEntry : IArchiveEntry
    {
        private readonly GZipStream gZipArchive;
        private readonly string archiveFileName;

        public GZipArchiveEntry(GZipStream gZipArchive, string archiveFileName)
        {
            this.gZipArchive = gZipArchive;
            this.archiveFileName = archiveFileName;
        }

        public string Name => Path.GetFileNameWithoutExtension(archiveFileName);

        public Stream GetStream()
        {
            return gZipArchive;
        }
    }
}
