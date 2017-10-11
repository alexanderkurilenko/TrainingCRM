using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public interface IArchiveManager
    {
        IEnumerable<string> AcceptedExtensions { get; }

        IArchiveReader GetArchiveReader(Stream archiveStream, string archiveFileName);

        IArchiveWriter GetArchiveWriter(Stream archiveStream, ArchiveWriterMode mode);
    }
}
