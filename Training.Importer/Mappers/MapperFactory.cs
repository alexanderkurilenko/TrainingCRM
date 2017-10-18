using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.ImportType;

namespace Training.Importer.Mappers
{
    public class MapperBase<TImportEntity,TCrmEntity>:IMapper
        where TImportEntity:ImportEntity 
        where TCrmEntity:Entity
    {
        public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

        public Func<Type, object> ServiceCtor => throw new NotImplementedException();



        public virtual TCrmEntity Map<TCrmEntity>(object source) => Mapper.Map<TCrmEntity>(source);

        public virtual TCrmEntity Map<TCrmEntity>(object source, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public virtual TCrmEntity Map<TImportEntity, TCrmEntity>(TImportEntity source)
        {
            throw new NotImplementedException();
        }

        public virtual TCrmEntity Map<TImportEntity, TCrmEntity>(TImportEntity source, Action<IMappingOperationOptions<TImportEntity, TCrmEntity>> opts)
        {
            throw new NotImplementedException();
        }

        public virtual TCrmEntity Map<TImportEntity, TCrmEntity>(TImportEntity source, TCrmEntity destination)
        {
            throw new NotImplementedException();
        }

        public virtual TCrmEntity Map<TImportEntity, TCrmEntity>(TImportEntity source, TCrmEntity destination, Action<IMappingOperationOptions<TImportEntity, TCrmEntity>> opts)
        {
            throw new NotImplementedException();
        }

        public virtual object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public virtual object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public virtual object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public virtual object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }
    }
}
