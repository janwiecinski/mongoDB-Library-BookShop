﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAcces.DAL.Models
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId _Id { get; set; }
    }
}
