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
            CreateMap<MetaCategory, SimpleMetaCategory>();
            CreateMap<MetaField, SimpleMetaField>();
            CreateMap<ListingMeta, SimpleListingMeta>();
            CreateMap<ListingStat, SimpleListingStat>();
            CreateMap<ListingReview, SimpleListingReview>();
        }
    }
}
