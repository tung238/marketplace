using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Web.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, SimpleUser>();
            CreateMap<Listing, SimpleListing>();
            CreateMap<Category, SimpleCategory>();
            CreateMap<Region, SimpleRegion>();
        }
    }
}
