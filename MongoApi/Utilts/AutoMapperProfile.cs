using AutoMapper;
using DataAcces.DAL.Models;
using MongoApi.Models;
using System.Collections.Generic;

namespace MongoApi.Utilts
{
    public class AutoMapperProfile: Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<Client, ClientViewModel>();
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<BookRent, BookRentViewModel>();
            CreateMap<BookRent, BookRentViewModel>().ReverseMap();
            CreateMap<BookCopy, BookCopyModel>();
            CreateMap<BookCopy, BookCopyModel>().ReverseMap();
            CreateMap<BookModel, BookViewModel>();
            CreateMap<BookModel, BookViewModel>().ReverseMap();
        }

    }
}
