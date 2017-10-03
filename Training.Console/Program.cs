using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Test test=new Test();
            test.Role = 0;
            System.Console.WriteLine(test.Role.ToString());
            System.Console.ReadKey();
        }
    }
}
