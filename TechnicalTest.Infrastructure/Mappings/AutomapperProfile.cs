using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Entities;

namespace TechnicalTest.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
