using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Ninject;

namespace Training.Core.DataAccess
{
    public partial class OrganizationServiceConte : IOrganizationServiceContext
    {
        private IOrganizationService organizationService;
        
        [Inject]
        public OrganizationServiceConte(OrganizationServiceFactory factory)
            : this(factory.Create())
        {

        }
       
        public OrganizationServiceConte(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

      

        public void Delete(Entity entity)
        {
            var deleteRequest = new OrganizationRequest { RequestName = "Delete" };

            var entrefDeleteTarget = new EntityReference
            {
                Id = entity.Id,
                LogicalName = entity.LogicalName.ToLower()
            };

            deleteRequest["Target"] = entrefDeleteTarget;
            Execute(deleteRequest);
        }

        public void Delete(string entityName, Guid recordId)
        {
            var deleteRequest = new OrganizationRequest { RequestName = "Delete" };

            var entrefDeleteTarget = new EntityReference
            {
                Id = recordId,
                LogicalName = entityName
            };

            deleteRequest["Target"] = entrefDeleteTarget;
            Execute(deleteRequest);
        }

        public Entity Retrieve(string entityName, Guid recordId)
        {
            Entity entity = null;

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet(true),
                Target = new EntityReference(entityName, recordId)
            };

            var response = (RetrieveResponse)Execute(request);

            if (response != null)
            {
                entity = (Entity)response.Entity;
            }

            return entity;
        }

        public string GetOptionSetTextFromValue(string optionSetValue, string optionSetLogicalName, string entityLogicalName)
        {
            string returnValue = string.Empty;

            if (!optionSetValue.Equals(string.Empty))
            {
                var request = new RetrieveAttributeRequest
                {
                    EntityLogicalName = entityLogicalName,
                    LogicalName = optionSetLogicalName,
                    RetrieveAsIfPublished = true
                };

                var response = (RetrieveAttributeResponse)Execute(request);

                if (response.AttributeMetadata is PicklistAttributeMetadata)
                {
                    var picklist = (PicklistAttributeMetadata)response.AttributeMetadata;
                    var picklistQuery = from option in picklist.OptionSet.Options
                                        where option.Value == int.Parse(optionSetValue)
                                        select option.Label.UserLocalizedLabel.Label;
                    returnValue = picklistQuery.FirstOrDefault();
                }
                else if (response.AttributeMetadata is StatusAttributeMetadata)
                {
                    var picklist = (StatusAttributeMetadata)response.AttributeMetadata;
                    var statusQuery = from option in picklist.OptionSet.Options
                                      where option.Value == int.Parse(optionSetValue)
                                      select option.Label.UserLocalizedLabel.Label;
                    returnValue = statusQuery.FirstOrDefault();
                }
                else if (response.AttributeMetadata is StateAttributeMetadata)
                {
                    var picklist = (StateAttributeMetadata)response.AttributeMetadata;
                    var stateQuery = from option in picklist.OptionSet.Options
                                     where option.Value == int.Parse(optionSetValue)
                                     select option.Label.UserLocalizedLabel.Label;
                    returnValue = stateQuery.FirstOrDefault();
                }
            }

            return returnValue;
        }

        public void AddObject(Entity entity)
        {
            organizationService.Create(entity);
        }

        public void DeleteObject(Entity entity)
        {
            organizationService.Delete(entity.LogicalName,entity.Id);
        }

        public IQueryable<T> CreateQuery<T>() where T : Entity
        {
            throw new NotImplementedException();
        }

        public SaveChangesResultCollection SaveChanges(SaveChangesOptions options)
        {
            throw new NotImplementedException();
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return organizationService.Execute(request);
        }

        public void UpdateObject(Entity entity)
        {
            organizationService.Update(entity);
        }

        public void DeleteLink(Entity source, Relationship relationship, Entity target)
        {
            throw new NotImplementedException();
        }

        public void AddLink(Entity source, Relationship relationship, Entity target)
        {
            throw new NotImplementedException();
        }

        public void AttachLink(Entity source, Relationship relationship, Entity target)
        {
            throw new NotImplementedException();
        }

        public bool IsAttached(Entity entity)
        {
            throw new NotImplementedException();
        }

        public bool IsAttached(Entity source, Relationship relationship, Entity target)
        {
            throw new NotImplementedException();
        }

        public void Attach(Entity entity)
        {
            
        }

        public bool Detach(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
