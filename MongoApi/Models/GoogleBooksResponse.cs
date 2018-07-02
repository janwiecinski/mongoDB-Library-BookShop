using System.Collections.Generic;

namespace MongoApi.Models
{

    public class GoogleBooksResponse
    {
        public List<GoogleBook> items { get; set; }
    }

    public class GoogleBook
    {
        public VolumeInfo volumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        public string title { get; set; }
    }
}
