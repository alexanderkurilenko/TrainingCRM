using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.Util.Archives;
using Training.Core.Util.FileSystem;
using Training.Importer.DataProccesor;
using Training.Importer.Deserializer;
using Training.Importer.ImportType.Models;
using Training.Importer.Infrastructure;

namespace Training.Importer.Ninject
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ImportFolderConfiguration>().ToMethod<ImportFolderConfiguration>(context => new ImportFolderConfiguration
            {
                InitialFolder = @"D:\Test",
                FailedFolder = @"D:\Test\Failed",
                ProcessingFolder = @"D:\Test\Processing",
                SucceedFolder = @"D:\Test\Succeed",
                ZippedFolder = @"D:\Test\Zipped"
            });
            Bind<ImportManager>().ToSelf();
            Bind<IFileSystem>().To<FileSystem>();
            Bind<IArchiveManagerFactory>().To<ArchiveManagerFactory>();
            Bind<IArchiveManager>().To<ZipArchiveManager>();
            Bind<IArchiveManager>().To<GZipArchiveManager>();

            Bind<IImportFileSystem>().To<ImportFileSystem>();

            Bind<IImportDeserializerFactory>().To<NinjectXmlDeserializerFactory>();
            Bind<IImportDataProcessorFactory>().To<ImportDataProcessorFactory>();
            Bind<IImportDeserializer>().To<GenericXmlImportDeserializer<PortalTests,PortalTest>>();

            Bind<IKernel>().ToMethod(context => context.Kernel).WhenInjectedInto<ImportDataProcessorFactory>();

        }
    }
}
