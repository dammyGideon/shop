using AutoMapper;
using orderService.domain.Entities;
using ShopCart.Dtos;

namespace ShopCart.Mapper
{
    public class BookProfiles : Profile
    {
        public BookProfiles()
        {
            CreateMap<Book, ReadBookDtos>();
        }
    }
}
