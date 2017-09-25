using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Training.Portals.Repositories
{
    public interface IRepository<T> where T:class 
    {
        IEnumerable<T> RetreiveAll();
        void Delete(Guid id);
        void Create(T item);
        T Get(Guid id);
        void Update(T item);
        List<EntityReference> GetEntityReference();

    }
}
