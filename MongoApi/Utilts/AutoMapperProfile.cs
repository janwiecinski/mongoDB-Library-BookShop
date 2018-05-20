using AutoMapper;
using DataAcces.DAL.Models;
using MongoApi.Models;

namespace MongoApi.Utilts
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Products, ProductViewModel>();
            CreateMap<BookModel, BookViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<BookRent, BookRentViewModel>();
        }

    }
}
