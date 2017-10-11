using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Training.Core.Util.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public FileStream OpenWrite(string filePath)
        {
            return File.OpenWrite(filePath);
        }

        public FileStream OpenRead(string filePath)
        {
            return File.OpenRead(filePath);
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public IEnumerable<string> GetFiles(string directory, string searchOptions)
        {
            return Directory.GetFiles(directory, searchOptions);
        }

        public void CopyFile(string sourceFilePath, string targetFilePath, bool overwrite)
        {
            File.Copy(sourceFilePath, targetFilePath, overwrite);
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public FileStream CreateFile(string filePath)
        {
            return File.Create(filePath);
        }

        public void MoveFile(string sourcePath, string destinationPath, bool overwrite)
        {
            CopyFile(sourcePath, destinationPath, overwrite);
            DeleteFile(sourcePath);
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public bool DirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return false;
            }

            return true;
        }

        public bool DirectoryPermissionsGranted(string directory)
        {
            try
            {
                var permission = new FileIOPermission(FileIOPermissionAccess.AllAccess, directory);
                permission.Demand();
            }
            catch (SecurityException)
            {
                return false;
            }

            return true;
        }

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}

