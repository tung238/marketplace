using AutoMapper;
using TNMarketplace.Core.Entities;

namespace TNMarketplace.Core.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, SimpleUser>();
            CreateMap<Listing, SimpleListing>();
            CreateMap<Category, SimpleCategory>();
            CreateMap<Region, SimpleRegion>();
            CreateMap<ListingPicture, PictureModel>();
        }
    }
}
