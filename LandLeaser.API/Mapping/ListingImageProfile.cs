using AutoMapper;
using LandLeaser.API.Models;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.API.Mapping
{
    public class ListingImageProfile : Profile
    {
        public ListingImageProfile()
        {
            CreateMap<ListingImage, ListingImageDto>();
            CreateMap<ListingImageDto, ListingImage>();
        }
    }
}
