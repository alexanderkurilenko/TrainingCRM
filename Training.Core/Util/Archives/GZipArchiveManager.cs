using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class GZipArchiveManager : IArchiveManager
    {
        public IEnumerable<string> AcceptedExtensions
        {
            get
            {
                return new List<string> { ".gz" };
            }
        }

        public IArchiveReader GetArchiveReader(Stream archiveStream, string archiveFileName)
        {
            return new GZipArchiveReader(archiveStream, archiveFileName);
        }

        public IArchiveWriter GetArchiveWriter(Stream archiveStream, ArchiveWriterMode mode)
        {
            throw new System.NotImplementedException();
        }
    }
}
