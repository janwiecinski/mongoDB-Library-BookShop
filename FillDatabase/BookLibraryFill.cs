using DataAcces.DAL.Models;
using MongoApi.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FillDatabase
{
    public class BookLibraryFill
    {
        
        public async Task<IList<BookModel>> InsertMany(IList<ObjectId> _IdList)
        {
            var titleList = new List<string>();

            using (var client = new HttpClient())
            {
                var url = "https://www.googleapis.com/books/v1/volumes?q=a&fields=items%2FvolumeInfo%2Ftitle&key=AIzaSyAJ2Jznnz5sEygEb_Yc5MLsbfq-sAx3tEI";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GoogleBooksResponse>(responseString);
                titleList = result.items.Select(s => s.volumeInfo.title).ToList();
            }
            var random = new Random();
       


            var bookList = new List<BookModel>();

            for (int i = 0; i < 100; i++)
            {
                var titleIndex = random.Next(titleList.Count);
                var authorIndex = random.Next(_IdList.Count);

                var book = new BookModel()
                {
                    Author_Id = _IdList[authorIndex],
                    Title = titleList[titleIndex],
                    BookCopyItems = new List<BookCopy>
                    {
                        new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 1
                        },
                         new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 2
                        },
                          new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 3
                        },
                           new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 4
                        },
                            new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 5
                        }
                    }
                };
                 bookList.Add(book);
            }
            return  bookList;
        }

      
    }
}
