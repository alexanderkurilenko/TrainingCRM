using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk.Client;
using Training.Core.DataAccess;
using Training.Core.Util.Archives;
using Training.Core.Util.FileSystem;
using Training.Importer;
using Training.Importer.DataProccesor;
using Training.Importer.Deserializer;
using Training.Importer.ImportType.Models;
using Training.Importer.Infrastructure;
using Ninject;

using Training.Importer.Ninject;

namespace Training.Console
{
    
    class Program
    {
        static void Main(string[] args)
        {
            

            
            User test = new User() { Name="f",Age=19,Hobby="d"};
            var users = new List<User>();
            users.Add(test);
            XmlSerializer formatter = new XmlSerializer(users.GetType());
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);

                System.Console.WriteLine("Объект сериализован");
            }
            //System.Console.WriteLine(d.UserId.ToString());
            //a.ProcessEntity(d);
            //g.Update(b);
            //c.Name = "1234";
            // a.ProcessEntity(c);
            //b.kurdev_name = "lolka";
            //var managers = new List<IArchiveManager>();
            //var zip=new ZipArchiveManager();
            //var gzip=new GZipArchiveManager();

            //managers.Add(zip);
            //managers.Add(gzip);

            //var config = new ImportFolderConfiguration
            //{
            //    InitialFolder = @"D:\Test",
            //    FailedFolder = @"D:\Test\Failed",
            //    ProcessingFolder = @"D:\Test\Processing",
            //    SucceedFolder = @"D:\Test\Succeed",
            //    ZippedFolder = @"D:\Test\Zipped"
            //};
            //var fileSystem=new ImportFileSystem(new FileSystem(), config, new ArchiveManagerFactory(managers));

            //var deserializers = new List<IImportDeserializer>();
            //deserializers.Add(new GenericXmlImportDeserializer<PortalTests,PortalTest>());
            //var deserializerFactory=new NinjectXmlDeserializerFactory(deserializers);

            //var processors = new List<IImportDataProcessor>();
            //processors.Add(a);
            //var man = new ImportManager(fileSystem, deserializerFactory,
            //    new ImportDataProcessorFactory());
            //man.RunImportIteration();

            // a.Update(b);
            log4net.Config.XmlConfigurator.Configure();
            var kernel = new StandardKernel(new Importer.Ninject.DataAccessModule(),new InfrastructureModule(),new DataProcessorModule());
            var importmanager = kernel.Get<ImportManager>();
            importmanager.RunImportIteration();
            System.Console.ReadKey();
        }
    }
}
