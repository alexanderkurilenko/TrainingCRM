using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Training.Core.DataAccess;

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
           var a=new PortalTestDataAccess();
            kurdev_portal_test b=new kurdev_portal_test();
            b.kurdev_Login = "lolo";
            b.kurdev_PassWord = "tesst_test";
            b.kurdev_name = "aliaksandr";
            a.Create(b);
            b.kurdev_name = "lolka";
            

            a.Update(b);
       
            System.Console.ReadKey();
        }
    }
}
