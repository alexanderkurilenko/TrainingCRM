using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.FileSystem
{
    public interface IFileSystem
    {
        FileStream OpenWrite(string filePath);
        FileStream OpenRead(string filePath);
        IEnumerable<string> GetFiles(string directory);
        IEnumerable<string> GetFiles(string directory, string searchOptions);
        void CopyFile(string sourceFilePath, string targetFilePath, bool overwrite = false);
        void DeleteFile(string filePath);
        FileStream CreateFile(string filePath);
        void MoveFile(string sourcePath, string destinationPath, bool overwrite);
        bool Exists(string filePath);
        bool DirectoryExists(string directory);
        bool DirectoryPermissionsGranted(string directory);
        string[] ReadAllLines(string filePath);
    }
}
