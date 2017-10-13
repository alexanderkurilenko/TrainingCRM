using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Training.Core.Util;
using Training.Core.Util.Archives;
using Training.Core.Util.FileSystem;

namespace Training.Importer.Infrastructure
{
    public class ImportFileSystem : IImportFileSystem
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ImportFileSystem));

        private readonly IFileSystem fileSystem;
        private readonly ImportFolderConfiguration importFolderConfiguration;
        private readonly IArchiveManagerFactory archiveManagerFactory;

        private const string doneFileExtension = ".done";
        private const string importFileSearchOption = "*.xml";

        private readonly ConcurrentDictionary<string, string> initialToProcessingPathMapping =
            new ConcurrentDictionary<string, string>();

        private readonly HashSet<string> capturedFiles = new HashSet<string>();

        public ImportFileSystem(IFileSystem fileSystem,
            ImportFolderConfiguration importFolderConfiguration, IArchiveManagerFactory archiveManagerFactory)
        {
            this.archiveManagerFactory = archiveManagerFactory;
            this.importFolderConfiguration = importFolderConfiguration;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<string> GetFiles()
        {
            var files = fileSystem.GetFiles(importFolderConfiguration.ProcessingFolder, importFileSearchOption).ToList();
            // .Where(file => !capturedFiles.Contains(file)).ToList();

            files.AddRange(fileSystem.GetFiles(importFolderConfiguration.InitialFolder).Where(DoneFileExistsForImportFile));

            if (files.Any())
            {
                return files;
            }

            UnpackZipArchives();
            files = fileSystem.GetFiles(importFolderConfiguration.InitialFolder).Where(DoneFileExistsForImportFile).ToList();

            return files;
        }

        public Stream GetFile(string filePath)
        {
            string movedFilePath = Path.Combine(importFolderConfiguration.ProcessingFolder, Path.GetFileName(filePath));

            if (movedFilePath != filePath)
            {
                fileSystem.CopyFile(filePath, movedFilePath);
                fileSystem.DeleteFile(filePath);

                fileSystem.CopyFile(Path.ChangeExtension(filePath, doneFileExtension),
                    Path.ChangeExtension(movedFilePath, doneFileExtension));
                fileSystem.DeleteFile(Path.ChangeExtension(filePath, doneFileExtension));
            }

            initialToProcessingPathMapping.TryAdd(filePath, movedFilePath);


            return fileSystem.OpenRead(movedFilePath);

            //var stream = fileSystem.OpenRead(movedFilePath);
            //var doc = new XmlDocument();
            //try
            //{
            //    doc.Load(stream);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    return stream;
            //}
            //catch { }

            //stream.Seek(0, SeekOrigin.Begin);

            //var streamCopy = new MemoryStream();
            //stream.CopyTo(streamCopy);
            //streamCopy.Seek(0, SeekOrigin.Begin);

            //using (var oReader = new StreamReader(streamCopy, Encoding.GetEncoding("ISO-8859-1")))
            //{
            //    doc.Load(oReader);
            //    return oReader.BaseStream;
            //}
        }

        public void MoveToFailed(string filePath)
        {
            MoveFromProcessingFolder(filePath, importFolderConfiguration.FailedFolder);
            logger.InfoFormat("File {0} was moved from processing to failed folder", Path.GetFileName(filePath));
        }

        public void MoveToSucceed(string filePath)
        {
            MoveFromProcessingFolder(filePath, importFolderConfiguration.SucceedFolder);
            logger.InfoFormat("File {0} was moved from processing to succeed folder", Path.GetFileName(filePath));
        }

        private void UnpackZipArchives()
        {
            var zipArchives = fileSystem.GetFiles(importFolderConfiguration.ZippedFolder);
            Console.WriteLine(zipArchives.Count());
            foreach (var zipArchive in zipArchives)
            {
                Console.WriteLine(zipArchive);
                var archiveManager = archiveManagerFactory.GetArchiveManagerForFile(zipArchive);
               
                using (var archive = archiveManager.GetArchiveReader(fileSystem.OpenRead(zipArchive), zipArchive))
                {
                    foreach (var archiveEntry in archive.GetTopLevelEntries())
                    {
                        UnpackArchiveEntry(archiveEntry, zipArchive);
                    }
                }

                fileSystem.DeleteFile(zipArchive);
                fileSystem.DeleteFile(Path.ChangeExtension(zipArchive, doneFileExtension));
            }
        }

        private void UnpackArchiveEntry(IArchiveEntry archiveEntry, string zipArchiveName)
        {
            var outputPath = Path.Combine(importFolderConfiguration.InitialFolder, archiveEntry.Name);
            Console.WriteLine(outputPath);
            try
            {
                using (var outputStream = fileSystem.OpenWrite(outputPath))
                {
                    var archiveStream = archiveEntry.GetStream();

                    StreamUtil.CopyAndClose(archiveStream, outputStream);
                }

                CreateDoneFileForUnpackedFile(Path.ChangeExtension(outputPath, doneFileExtension));
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Error during archive entry unpack. Archive {0}, entry{1}", zipArchiveName,
                    archiveEntry.Name), exception);
            }
        }

        private void CreateDoneFileForUnpackedFile(string file)
        {
            var doneFilePath = Path.ChangeExtension(file, doneFileExtension);
            var fileStream = fileSystem.CreateFile(doneFilePath);
            fileStream.Close();
        }

        private void MoveFromProcessingFolder(string initialFilePath, string folder)
        {
            var processingFilePath = initialToProcessingPathMapping[initialFilePath];

            // capturedFiles.Remove(processingFilePath);

            fileSystem.MoveFile(processingFilePath, Path.Combine(folder, Path.GetFileName(processingFilePath)), true);

            string tempFilePath = null;

            initialToProcessingPathMapping.TryRemove(initialFilePath, out tempFilePath);

            fileSystem.DeleteFile(Path.ChangeExtension(processingFilePath, doneFileExtension));
        }

        private bool DoneFileExistsForImportFile(string file)
        {
            return Path.GetExtension(file) != doneFileExtension && fileSystem.Exists(Path.ChangeExtension(file, doneFileExtension));
        }
    }
}
