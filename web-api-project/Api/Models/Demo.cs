using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models 
{
    public class Demo 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

    }
}