using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.Archives
{
    public class ZipArchiveWriter : IArchiveWriter
    {
        protected readonly ZipArchive zipArchive;

        public ZipArchiveWriter(Stream zipStream, ArchiveWriterMode mode)
        {
            zipArchive = new ZipArchive(zipStream, GetMode(mode), true);
        }

        public void AddToArchive(Stream inputStream, string entryName)
        {
            var entry = zipArchive.CreateEntry(entryName, CompressionLevel.Fastest);
            using (var entryStream = entry.Open())
            {
                StreamUtil.Copy(inputStream, entryStream);
            }
        }

        public void Dispose()
        {
            zipArchive.Dispose();
        }

        private ZipArchiveMode GetMode(ArchiveWriterMode mode)
        {
            switch (mode)
            {
                case ArchiveWriterMode.Create:
                    return ZipArchiveMode.Create;
                case ArchiveWriterMode.Update:
                    return ZipArchiveMode.Update;
                default:
                    throw new Exception("Arhive writer mode not supported");
            }
        }
    }
}
