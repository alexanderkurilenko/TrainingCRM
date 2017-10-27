using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.DataAccess;
using Training.Importer.ImportType.Models;

namespace Training.Importer.RecordFinders
{
    public class ContactRecordFinder : IExistingRecordFinder<Training.Importer.ImportType.Models.Contact, Contact>
    {
        protected readonly ContactDataAccess contactDataAccess;

        public ContactRecordFinder(ContactDataAccess _contactDataAccess)
        {
            this.contactDataAccess = _contactDataAccess;
        }

        public void Dispose()
        {
           contactDataAccess.Dispose();
        }

        public Contact FindExistingRecord(ImportType.Models.Contact importEntity)
        {
            if (importEntity.ContactId != Guid.Empty)
            {
                var res = contactDataAccess.GetById(importEntity.ContactId);
                Console.WriteLine("Contact has been found");
                if (res != null)
                    contactDataAccess.Detach(res);
                return res;
            }
            return null; 
        }
    }
}
