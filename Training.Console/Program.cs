﻿using System;
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

namespace Training.Console
{
    public enum A
    {
        User,
        Admin
    }

    public class Test
    {
        public A Role { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var a = new PortalTestDataProcessor();
            var g=new PortalTestDataAccess();
            kurdev_portal_test b=new kurdev_portal_test();
            b.Id= new Guid("d539dda8-47af-e711-b87e-60a44c7256ed");
            b.kurdev_Login = "1234";
            b.kurdev_PassWord = "topchick";
            b.kurdev_name = "123525";
            PortalTests tests=new PortalTests();
            PortalTest d=new PortalTest();
            //d.UserId = new Guid("d539dda8-47af-e711-b87e-60a44c7256ed");
            d.UserId = new Guid("d539dda8-47af-e711-b87e-60a44c7256ed");
            d.Login = "1234";
            d.Password = "8f4";
            d.Name = "topchicktopchicktopchick";
            d.Role = Roles.Admin;
            PortalTest[] arr = { d };
            tests.PortalTest = arr;
            XmlSerializer formatter = new XmlSerializer(typeof(PortalTests));
           
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tests);

                System.Console.WriteLine("Объект сериализован");
            }
            //System.Console.WriteLine(d.UserId.ToString());
            //a.ProcessEntity(d);
            //g.Update(b);
            //c.Name = "1234";
            // a.ProcessEntity(c);
            //b.kurdev_name = "lolka";
            var managers = new List<IArchiveManager>();
            var zip=new ZipArchiveManager();
            var gzip=new GZipArchiveManager();
           
            managers.Add(zip);
            managers.Add(gzip);

            var config = new ImportFolderConfiguration
            {
                InitialFolder = @"D:\Test",
                FailedFolder = @"D:\Test\Failed",
                ProcessingFolder = @"D:\Test\Processing",
                SucceedFolder = @"D:\Test\Succeed",
                ZippedFolder = @"D:\Test\Zipped"
            };
            var fileSystem=new ImportFileSystem(new FileSystem(), config, new ArchiveManagerFactory(managers));

            var deserializers = new List<IImportDeserializer>();
            deserializers.Add(new GenericXmlImportDeserializer<PortalTests,PortalTest>());
            var deserializerFactory=new NinjectXmlDeserializerFactory(deserializers);

            var processors = new List<IImportDataProcessor>();
            processors.Add(a);
            var man = new ImportManager(fileSystem, deserializerFactory,
                new ImportDataProcessorFactory());
            man.RunImportIteration();

            // a.Update(b);

            System.Console.ReadKey();
        }
    }
}
