using DataAcces.DAL.Models;
using System;
using System.Linq;
using MongoDB.Driver;

namespace FillDatabase
{
    public class Program
    {
        public Program()
        {
          
        }

        static void Main(string[] args)
        {
            var data = new DataBaseConnect();
            var authorCollection = data.Database.GetCollection<Author>("Author");
            var bookModelCollection = data.Database.GetCollection<BookModel>("BookModel");

            var authorsList = AuthorFill.InsertManyAuthors();
            authorCollection.InsertMany(authorsList);
            var objectIdAuthor = authorCollection.Find(p=>true).ToList().Select(p=>p._Id).ToList();

            var mBookFill = new BookLibraryFill();
            var result =  mBookFill.InsertMany(objectIdAuthor).Result;


            bookModelCollection.InsertMany(result);
            Console.WriteLine(result);
            Console.ReadKey();

        }




    }
}
