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
       public static void GetMappers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PortalTest, kurdev_portal_test>()
           .ForMember("kurdev_Login", opt => opt.MapFrom(c => c.Login))
           .ForMember("kurdev_name", opt => opt.MapFrom(c => c.Name))
           .ForMember("kurdev_PassWord", opt => opt.MapFrom(c => c.Password))
           .ForMember("kurdev_Role", opt => opt.MapFrom(c => new OptionSetValue((int)c.Role)))
           .ForMember("kurdev_portal_testId", opt => opt.MapFrom(c => c.UserId == Guid.Empty ? Guid.NewGuid() : c.UserId)));
        }
    }
}
