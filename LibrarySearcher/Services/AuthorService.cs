using AutoMapper;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using LibrarySearcher.Models;
using MongoDB.Bson;

namespace LibrarySearcher.Services
{
    public class AuthorService: IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;

        public  AuthorService(IRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public AuthorViewModel GetById(string Id)
        {
            var author = _repository.GetItem(new ObjectId(Id));
            var result = _mapper.Map<Author, AuthorViewModel>(author);

            return result;
        }
    }
}
