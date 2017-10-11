using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Training.Core.DataAccess
{
    public interface IOrganizationServiceContext : IDisposable
    {
        void AddObject(Entity entity);

        void DeleteObject(Entity entity);

        IQueryable<T> CreateQuery<T>() where T : Entity;

        SaveChangesResultCollection SaveChanges(SaveChangesOptions options);

        OrganizationResponse Execute(OrganizationRequest request);

        void UpdateObject(Entity entity);

        void DeleteLink(Entity source, Relationship relationship, Entity target);

        void AddLink(Entity source, Relationship relationship, Entity target);

        void AttachLink(Entity source, Relationship relationship, Entity target);

        bool IsAttached(Entity entity);

        bool IsAttached(Entity source, Relationship relationship, Entity target);

        void Attach(Entity entity);

        bool Detach(Entity entity);
    }
}
