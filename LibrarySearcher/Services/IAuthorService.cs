using LibrarySearcher.Models;

namespace LibrarySearcher.Services
{
    public interface IAuthorService
    {
        AuthorViewModel GetById(string Id);
    }
}
