using AutoMapper;
using LandLeaser.API.Models;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.API.Mapping
{
    public class ListingProfile : Profile
    {
        public ListingProfile()
        {
            CreateMap<ListingImage, ListingImageDto>();
            CreateMap<ListingImageDto, ListingImage>();

            CreateMap<Listing, GetListingDto>();
            CreateMap<GetListingDto, Listing>();
        }
    }
}
