using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Importer.ImportType;
using Training.Importer.ImportType.Models;

namespace Training.Importer.Mappers
{
    public static class MapperBase      
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

        public static void GetMappers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PortalTest, kurdev_portal_test>()
  
            
           .ForMember("kurdev_Login", opt => opt.MapFrom(c => c.Login))
           .ForMember("kurdev_name", opt => opt.MapFrom(c => c.Name))
           .ForMember("kurdev_PassWord", opt => opt.MapFrom(c => c.Password))
           .ForMember("kurdev_Role", opt => opt.MapFrom(c => new OptionSetValue((int)c.Role)))
           .ForMember("kurdev_portal_testId", opt => opt.MapFrom(c => c.UserId)));

            Mapper.Initialize(cfg => cfg.CreateMap<ImportType.Models.Contact, Contact>()
            .IgnoreAllUnmapped()
            .ForMember("ContactId",opt=>opt.MapFrom(c=>c.ContactId))
            .ForMember("FullName",opt=>opt.MapFrom(c=>c.FullName))
            .ForMember("CreatedOn",opt=>opt.MapFrom(c=>c.CreatedOn))
            );
         
        }
    }
}
