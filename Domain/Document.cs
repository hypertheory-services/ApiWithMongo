using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiWithMongo.Domain;

public class Document
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
}
