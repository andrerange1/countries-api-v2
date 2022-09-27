using AutoMapper;
using Countries.Contracts.DTOs;
using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Contracts.Maps
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Country, CountryResponse>().ReverseMap();
            CreateMap<Country, CountryDetailResponse>().ReverseMap();
        }

    }
}
