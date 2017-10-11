using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class ZipArchiveManager : IArchiveManager
    {
        public IEnumerable<string> AcceptedExtensions
        {
            get { return new List<string> { ".zip" }; }
        }

        public IArchiveReader GetArchiveReader(Stream archiveStream, string archiveFileName)
        {
            return new ZipArchiveReader(archiveStream);
        }

        public IArchiveWriter GetArchiveWriter(Stream archiveStream, ArchiveWriterMode mode)
        {
            return new ZipArchiveWriter(archiveStream, mode);
        }
    }
}
