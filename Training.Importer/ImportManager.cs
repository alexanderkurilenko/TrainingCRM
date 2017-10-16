using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Training.Importer.DataProccesor;
using Training.Importer.Deserializer;
using Training.Importer.ImportType;
using Training.Importer.Infrastructure;

namespace Training.Importer
{
    public class ImportManager
    {
        private readonly IImportFileSystem filesProvider;
        private readonly IImportDeserializerFactory serializerFactory;
     
        private readonly IImportDataProcessorFactory importDataProcessorFactory;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly ILog logger = LogManager.GetLogger(typeof(ImportManager));

        private ConcurrentDictionary<string, string> takenFiles;

        public int ParallelFilesCount { get; set; }

        public ImportManager(IImportFileSystem filesProvider, IImportDeserializerFactory serializerFactory
         , IImportDataProcessorFactory importDataProcessorFactory)
        {
            this.filesProvider = filesProvider;
            this.serializerFactory = serializerFactory;
            this.importDataProcessorFactory = importDataProcessorFactory;

            cancellationTokenSource = new CancellationTokenSource();
            takenFiles = new ConcurrentDictionary<string, string>();

            ParallelFilesCount = 200; // default value
        }

        public void RunImportIteration()
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            var files = filesProvider.GetFiles().OrderByDescending(file => file);
            List<KeyValuePair<string, string>> notTakenFiles;

            lock (takenFiles)
            {
                var filesCanBeTakenCount = ParallelFilesCount - takenFiles.Count;
                notTakenFiles = new List<KeyValuePair<string, string>>(files.Where(file => !takenFiles.ContainsKey(Path.GetFileName(file)))
                    .Distinct(new FileNameEqualityComparer())
                    .Take(filesCanBeTakenCount)
                    .Select(file => new KeyValuePair<string, string>(Path.GetFileName(file), file)));
                notTakenFiles.ForEach(nonTakenFile => takenFiles.TryAdd(nonTakenFile.Key, nonTakenFile.Value));
            }

            Parallel.ForEach(notTakenFiles.Select(notTakenFile => notTakenFile.Value), ProcessFileEx);
        }

        public void StopProcessing()
        {
            cancellationTokenSource.Cancel();
        }

        private void ProcessFileEx(string fileName)
        {
            try
            {
                ProcessFile(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The fatal error occurs during file {0} processing. Error: {1}. {2}",
                    Path.GetFileName(fileName), ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    ex.StackTrace);
            }
            finally
            {
                RemoveTakenFile(fileName);
            }
        }

        private void ProcessFile(string fileName)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            Console.WriteLine("Import of {0} file is started.", Path.GetFileName(fileName));

            var startTime = DateTime.Now;
            var messageBuilder = new StringBuilder();
            string sourceSystem = string.Empty;
            int totalRecordsCount = 0, failedRecordsCount = 0, successRecordsCount = 0;

            try
            {
                using (var stream = filesProvider.GetFile(fileName))
                {
                    var serializer = serializerFactory.GetDeserializer(stream);



                    stream.Seek(0, SeekOrigin.Begin);
                    Console.WriteLine(serializer.AcceptedClass);
                    var entities = serializer.Deserialize(stream).ToList();
                    Console.WriteLine(entities.Count);
                    if (entities.Count > 0)
                    {
                        var entity = entities[0];
                        
                        ProcessEntitiesCollection(entities, messageBuilder, out failedRecordsCount,
                            out successRecordsCount);
                        totalRecordsCount = entities.Count;
                    }
                    else
                    {
                        messageBuilder.AppendFormat("No entities for import are found in file {0}", Path.GetFileName(fileName));
                    }
                }

                string warningMessage = messageBuilder.ToString();

                if (!string.IsNullOrWhiteSpace(warningMessage))
                {
                    var logName = FormatCrmLogName(fileName);

                    filesProvider.MoveToFailed(fileName);
                    RemoveTakenFile(fileName);

                  

                    return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Fatal error is occured while processing file: {0}.", Path.GetFileName(fileName)), exception.Data);
                filesProvider.MoveToFailed(fileName);
                RemoveTakenFile(fileName);
                throw;

                return;
            }

            string logMessage = string.Format("File {0} was imported successfully.", Path.GetFileName(fileName));

            logger.Info(logMessage);
            filesProvider.MoveToSucceed(fileName);
            RemoveTakenFile(fileName);

        }

        private void ProcessEntitiesCollection(List<ImportEntity> entities, StringBuilder messageBuilder, out int failedRecordsCount,
            out int successRecordsCount)
        {
           
            var processor = importDataProcessorFactory.GetImportDataProcessor(entities.ElementAt(0));

            successRecordsCount = 0;
            failedRecordsCount = 0;
         
            for (int index = 0; index < entities.Count; ++index)
            {
                try
                {
                   
                    processor.ProcessEntity(entities[index]);
                    successRecordsCount++;
                }
                catch (Exception exception)
                {
                   
                    AppendErrorToCrmErrorMessage(messageBuilder, exception, index);
                    failedRecordsCount++;
                    throw;
                }
            }
        }

        private void AppendErrorToCrmErrorMessage(StringBuilder errorMessageBuilder, Exception exception, int elementIndex)
        {
            string commonErrorMessage = string.Format("The error occurs while processing element {0}.", elementIndex + 1);

            logger.Error(commonErrorMessage, exception);
            logger.Error(exception);
          
        }

        private string FormatCrmLogName(string fileName)
        {
            return string.Format("{0} {1}", DateTime.Today.ToString("yyyy-MM-dd"), Path.GetFileName(fileName));
        }

        private void RemoveTakenFile(string fileName)
        {
            lock (takenFiles)
            {
                var file = Path.GetFileName(fileName);

                if (string.IsNullOrWhiteSpace(file))
                {
                    return;
                }

                if (takenFiles.ContainsKey(file))
                {
                    string foundFileName;
                    takenFiles.TryRemove(file, out foundFileName);
                }
            }
        }
    }
}
