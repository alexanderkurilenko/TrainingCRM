using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Training.Core.DataAccess;
using Training.Importer.DataProccesor;
using Training.Importer.ImportType.Models;

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
            PortalTest d=new PortalTest();
            //d.UserId = new Guid("d539dda8-47af-e711-b87e-60a44c7256ed");
            d.UserId = new Guid("d539dda8-47af-e711-b87e-60a44c7256ed");
            d.Login = "1234";
            d.Password = "8f4";
            d.Name = "topchicktopchicktopchick";
            d.Role = Roles.Admin;
            //System.Console.WriteLine(d.UserId.ToString());
            a.ProcessEntity(d);
            //g.Update(b);
            //c.Name = "1234";
           // a.ProcessEntity(c);
            //b.kurdev_name = "lolka";


            // a.Update(b);

            System.Console.ReadKey();
        }
    }
}
