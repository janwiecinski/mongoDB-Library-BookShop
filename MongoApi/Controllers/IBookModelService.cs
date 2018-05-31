﻿using DataAcces.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    public interface IBookModelService
    {
        IEnumerable<BookModel> GetItems();
        BookModel GetItem(ObjectId id);
        BookModel Create(BookModel item);
        void Update(BookModel item);
        void Remove(ObjectId id);
        BookModel BookRent(Client client, BookModel book);
        void InsertMany();
    }
}
