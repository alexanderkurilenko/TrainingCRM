using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.DataAccess;
using Training.Importer.ImportType.Models;
using Training.Importer.Mappers;
using Training.Importer.RecordFinders;
using Training.Importer.UpdateRuleChecker;

namespace Training.Importer.DataProccesor
{
    public class ContactDataProcessor : ImportDataProcessorWithUpdate<Training.Importer.ImportType.Models.Contact, Contact>, IDisposable
    {
        private readonly ContactDataAccess contactDataAccess;

        public ContactDataProcessor(ContactDataAccess _contactDataAccess):base(new ContactRecordFinder(_contactDataAccess),new AlwaysTrueUpdateRuleChecker<Training.Importer.ImportType.Models.Contact,Contact>())
        {
            contactDataAccess = _contactDataAccess;
        }

        public void Dispose()
        {
            contactDataAccess?.Dispose();
        }

        protected override Contact CreateNew(ImportType.Models.Contact importEntity)
        {
            var contact = new Contact();
            SetData(ref contact, importEntity);
            contactDataAccess.Create(contact);
            return contact;
        }

        protected override Contact UpdateExisting(Contact crmEntity, ImportType.Models.Contact importEntity)
        {
            SetData(ref crmEntity, importEntity);
            contactDataAccess.Detach(crmEntity);
            contactDataAccess.Update(crmEntity);
            return crmEntity;
        }
       
        private void SetData(ref Contact entity, ImportType.Models.Contact contact)
        {

            // Mapper.Initialize(cfg => cfg.CreateMap<ImportType.Models.Contact, Contact>());

            //MapperBase.GetMappers();

            //entity = Mapper.Map<ImportType.Models.Contact, Contact>(contact);
            entity.ContactId = contact.ContactId;
            entity.FirstName = contact.FirstName;
            entity.LastName = contact.LastName;

        }
      
    }
}
