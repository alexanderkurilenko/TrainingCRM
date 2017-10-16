using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Ninject;
using Training.Core.Util;

namespace Training.Core.DataAccess
{
    public class GenericDataAccess<T>:IDisposable where T : Entity
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(GenericDataAccess<T>));

        //protected IOrganizationServiceContext service;
        protected  OrganizationServiceContext service;

       
        [Inject]
        public GenericDataAccess(OrganizationServiceContext  serv)
        {
            this.service =serv;
        }

        public virtual T GetById(Guid id)
        {
            return (from entity in GetAll()
                    where entity.Id == id
                    select entity).SingleOrDefault();
        }

        public virtual bool Exists(Guid id)
        {
            return (from entity in GetAll()
                    where entity.Id == id
                    select entity.Id).SingleOrDefault() != default(Guid);
        }

        public virtual IQueryable<T> GetAll()
        {
            return service.CreateQuery<T>();
        }

        public void SetState(T entity, int stateValue)
        {
            var request = new SetStateRequest
            {
                State = new OptionSetValue(stateValue),
                EntityMoniker = new EntityReference(entity.LogicalName, entity.Id)
            };

           service.Execute(request);
        }

        public void SetStatusAndState(T entity, int statusValue, int stateValue)
        {
            var request = new SetStateRequest
            {
                Status = new OptionSetValue(statusValue),
                EntityMoniker = new EntityReference(entity.LogicalName, entity.Id),
                State = new OptionSetValue(stateValue)
            };

           service.Execute(request);
        }

     

        public void Create(T entity)
        {
            try
            {
               service.AddObject(entity);

               service.SaveChanges(SaveChangesOptions.None);
            }
            catch (Exception)
            {
                // if entity was attached to service, but error occured while creating this entity, we must deattach entity from service to be able to create entities with this service
                if (service.IsAttached(entity))
                {
                   service.Detach(entity);
                }

                throw;
            }
        }

        public void Update(T entity)
        {
           // service.Detach(entity);
            try
            {
                if (!service.IsAttached(entity))
                {
                    service.Attach(entity);
                }
                service.UpdateObject(entity);
                service.SaveChanges(SaveChangesOptions.None);
               
            }
            catch (Exception)
            {
                // if entity was attached to service, but error occured while creating this entity, we must deattach entity from service to be able to create entities with this service
                if (service.IsAttached(entity))
                {
                    service.Detach(entity);
                }

                
                throw;
            }
        }

        public void Delete(T entity)
        {
            if (!service.IsAttached(entity))
            {
                service.Attach(entity);
            }

            service.DeleteObject(entity);
            service.SaveChanges(SaveChangesOptions.None);
        }

        public SaveChangesResultCollection SaveChanges(SaveChangesOptions options)
        {
            return service.SaveChanges(options);
        }

        public SaveChangesResultCollection SaveChanges()
        {
            return service.SaveChanges(SaveChangesOptions.None);
        }

        public virtual IEnumerable<T> Fetch(string fetchXml)
        {
            var conversionRequest = new FetchXmlToQueryExpressionRequest { FetchXml = fetchXml };
            var conversionResponse = (FetchXmlToQueryExpressionResponse)service.Execute(conversionRequest);
            var retrieveRequest = new RetrieveMultipleRequest { Query = conversionResponse.Query };
            var retrieveResponse = (RetrieveMultipleResponse)service.Execute(retrieveRequest);

            return retrieveResponse.EntityCollection.Entities.Select(x => (T)x);
        }

        public virtual IEnumerable<T> FetchAll(string fetchXml)
        {
            RetrieveMultipleResponse allRecords = null;
            var conversionRequest = new FetchXmlToQueryExpressionRequest { FetchXml = fetchXml };
            var conversionResponse = (FetchXmlToQueryExpressionResponse)service.Execute(conversionRequest);
            var retrieveRequest = new RetrieveMultipleRequest { Query = conversionResponse.Query };

            // add paging to request:
            int i = 1;
            ((Microsoft.Xrm.Sdk.Query.QueryExpression)(retrieveRequest.Query)).PageInfo.Count = 5000;
            ((Microsoft.Xrm.Sdk.Query.QueryExpression)(retrieveRequest.Query)).PageInfo.PageNumber = i;

            bool moreRecords = true;
            do
            {
                var retrieveResponse = (RetrieveMultipleResponse)service.Execute(retrieveRequest);
                if (i == 1)
                {
                    allRecords = retrieveResponse;
                }
                else
                {
                    allRecords.EntityCollection.Entities.AddRange(retrieveResponse.EntityCollection.Entities.Select(x => (T)x));
                }
                moreRecords = retrieveResponse.EntityCollection.MoreRecords;
                i++;
                ((Microsoft.Xrm.Sdk.Query.QueryExpression)(retrieveRequest.Query)).PageInfo.PageNumber = i;

            } while (moreRecords);

            return allRecords.EntityCollection.Entities.Select(x => (T)x);
        }

        public void AddLink(Entity source, Relationship relationship, Entity target)
        {
            if (!service.IsAttached(source))
            {
                service.Attach(source);
            }

            if (!service.IsAttached(target))
            {
                service.Attach(target);
            }

            service.AddLink(source, relationship, target);
        }

        public void DeleteLink(Entity source, Relationship relationship, Entity target)
        {
            if (!service.IsAttached(source))
            {
                service.Attach(source);
            }

            if (!service.IsAttached(target))
            {
                service.Attach(target);
            }

            service.DeleteLink(source, relationship, target);
        }

        public void Detach(Entity entity)
        {
            if (service.IsAttached(entity))
            {
                service.Detach(entity);
            }
        }

        public void SendRecordToQueue(Entity entity, Queue queue)
        {
            var addToQueueRequest = new AddToQueueRequest
            {
                DestinationQueueId = queue.Id,
                Target = new EntityReference(entity.LogicalName, entity.Id)
            };

            service.Execute(addToQueueRequest);
        }

        public IEnumerable<T> ExecuteQuery(QueryExpression queryExpression, int? recordsCont = null)
        {
            queryExpression.PageInfo = new PagingInfo { PageNumber = 1, Count = recordsCont != null ? recordsCont.Value : 1000 };
            var request = new RetrieveMultipleRequest { Query = queryExpression };
            var result = new List<Entity>();

            while (true)
            {
                logger.InfoFormat("Fetching page='{0}' of the {1} records. Records count='{2}'", queryExpression.PageInfo.PageNumber,
                    queryExpression.EntityName, queryExpression.PageInfo.Count);
                var response = (RetrieveMultipleResponse)service.Execute(request);
                if (response.EntityCollection.Entities != null)
                {
                    response.EntityCollection.Entities.ForEach(result.Add);
                }

                if (recordsCont != null)
                {
                    break;
                }

                if (response.EntityCollection.MoreRecords)
                {
                    queryExpression.PageInfo.PageNumber++;
                    queryExpression.PageInfo.PagingCookie = response.EntityCollection.PagingCookie;
                }
                else
                {
                    break;
                }
            }

            logger.InfoFormat("Fetching of {0} records is finished", queryExpression.EntityName);
            return result.Select(x => (T)x);
        }

        public IEnumerable<T> GetDuplicates(T entity)
        {
            var duplicatesRequest = new RetrieveDuplicatesRequest
            {
                BusinessEntity = entity,
                MatchingEntityName = entity.LogicalName,
                PagingInfo = new PagingInfo { PageNumber = 1, Count = 100 }
            };

            var duplicatesResponse = (RetrieveDuplicatesResponse)service.Execute(duplicatesRequest);

            return duplicatesResponse.DuplicateCollection.Entities.Cast<T>()
                .Where(duplicate => duplicate.Attributes.Contains("statecode") && ((OptionSetValue)duplicate.Attributes["statecode"]).Value == 0);
        }

        public void AssignOwner(T ownedEntity, Entity owner)
        {
            AssignOwner(ownedEntity, owner.ToEntityReference());
        }

        public void AssignOwner(T entity, EntityReference owner)
        {
            var assignRequest = new AssignRequest() { Assignee = owner, Target = entity.ToEntityReference() };

            service.Execute(assignRequest);
        }

        public void Dispose()
        {
            service.Dispose();
            
        }
    }
}
