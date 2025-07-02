using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SE4458FinalProject.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // admin, company, user
    }
} 