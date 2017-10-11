using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util
{
    public static class StreamUtil
    {
        public static void Copy(Stream sourceStream, Stream targetStream, byte[] buffer)
        {
            int size = sourceStream.Read(buffer, 0, buffer.Length);

            while (size > 0)
            {
                targetStream.Write(buffer, 0, size);
                size = sourceStream.Read(buffer, 0, buffer.Length);
            }
        }

        public static void Copy(Stream sourceStream, Stream targetStream)
        {
            Copy(sourceStream, targetStream, new byte[4096]);
        }

        public static void CopyAndClose(Stream sourceStream, Stream targetStream)
        {
            Copy(sourceStream, targetStream);

            sourceStream.Close();
            targetStream.Close();
        }
    }
}
