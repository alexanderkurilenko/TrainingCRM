using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{

    public class ZipArchiveEntry : IArchiveEntry
    {
        //public ZipArchiveEntry Entry { get; private set; }
        //public GZipStreamEntry { get; private set; }
        public System.IO.Compression.ZipArchiveEntry Entry { get; private set; }

        public ZipArchiveEntry(System.IO.Compression.ZipArchiveEntry zipArchiveEntry)
        {
            Entry = zipArchiveEntry;
        }

        public string Name
        {
            get
            {
                return Entry.Name;
            }
        }

        public Stream GetStream()
        {
            return Entry.Open();
        }
    }
}
