using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.Infrastructure
{
    public struct LockWithRefCounter
    {
        public object LockObject { get; set; }
        public int RefCounter { get; set; }
    }
}
