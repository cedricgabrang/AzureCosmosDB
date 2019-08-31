using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureCosmosDBDemo
{
    public class TechEventsModel
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("eventName")]
        public string Event { get; set; }

        [BsonElement("eventLocation")]
        public string Location { get; set; }

        [BsonElement("eventDateTime")]
        public string When { get; set; }
    }
}
