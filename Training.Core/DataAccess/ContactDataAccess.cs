using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Ninject;

namespace Training.Core.DataAccess
{
    public class ContactDataAccess : GenericDataAccess<Contact>
    {
        [Inject]
        public ContactDataAccess(OrganizationServiceContext serv) : base(serv)
        {
        }

        public IEnumerable<Contact> GetContactByFullName(string fullName)
        {
            return (from c in service.CreateQuery<Contact>()
                    where c.FullName == fullName
                    select c);
        }
    }
}
