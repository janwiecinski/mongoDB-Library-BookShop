using System.Threading.Tasks;

namespace LibrarySearcher.Services
{
    public interface IAmazonSendService
    {
        void CreateClient(object o);

        Task WritingAnObjectAsync(object o);
    }
}
