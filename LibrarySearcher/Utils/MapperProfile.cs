using AutoMapper;
using DataAcces.DAL.Models;
using LibrarySearcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace LibrarySearcher.Utils
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Author, AuthorViewModel>().ForMember(x => x.Id, m => m.ResolveUsing(x => x._Id.ToString())); 
            CreateMap<AuthorViewModel, Author>().ForMember(x => x._Id, m => m.ResolveUsing(x => ObjectId.Parse(x.Id)));
            CreateMap<Client, ClientViewModel>().ForMember(x => x.Id, m => m.ResolveUsing(x => x._Id.ToString()));
            CreateMap<ClientViewModel, Client>().ForMember(x => x._Id, m => m.ResolveUsing(x => ObjectId.Parse(x.Id)));
            CreateMap<BookRent, BookRentViewModel>();
            CreateMap<BookRentViewModel, BookRent>();
            CreateMap<BookCopy, BookCopyModel>();
            CreateMap<BookCopyModel, BookCopy>();
            CreateMap<BookModel, BookViewModel>().ForMember(x => x.Id, m => m.ResolveUsing(x => x._Id.ToString())).ForMember(x => x.AuthorId, m => m.ResolveUsing(x => x.Author_Id.ToString())); 
            CreateMap<BookViewModel, BookModel>().ForMember(x => x._Id, m => m.ResolveUsing(x => ObjectId.Parse(x.Id))).ForMember(x => x.Author_Id, m => m.ResolveUsing(x => ObjectId.Parse(x.AuthorId)));
        }
    }
}
