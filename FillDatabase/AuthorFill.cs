using DataAcces.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FillDatabase
{
    public static class AuthorFill
    {

        public static IList<Author> InsertManyAuthors()
        {
            var nameTable = new List<string>() { "Jasiek", "Piotr", "Kasia", "Marcel", "Krzyś", "Ola", "Paweł", "Tosia", "Jacek" };
            var lastNameTable = new List<string>() { "Smith", "Pop", "Głow", "Marcelski", "Krzy", "Olanski", "Pawelski", "Tosiak", "Jacek" };
            var authorList = new List<Author>();

            for (int i = 0; i < 20; i++)
            {
                var random = new Random();
                int nameElement = random.Next(nameTable.Count);
                int lastNameElement = random.Next(lastNameTable.Count);
                var author = new Author() { FirstName = nameTable[nameElement], LastName = lastNameTable[lastNameElement] };
                authorList.Add(author);
            }
            return authorList;
        }
    }
}
