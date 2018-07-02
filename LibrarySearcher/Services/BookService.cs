using AutoMapper;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using LibrarySearcher.Models;
using System.Collections.Generic;

namespace LibrarySearcher.Services
{
    public class BookService: IBookService
    {
        private readonly IRepository<BookModel> _repository;
        private readonly IMapper _mapper;

        public BookService(IRepository<BookModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<BookViewModel> GetBooks()
        {
            var books = _repository.GetItems();
            var result = _mapper.Map<IEnumerable<BookModel>, IEnumerable<BookViewModel>>(books);
            return result;
        }
    }
}
