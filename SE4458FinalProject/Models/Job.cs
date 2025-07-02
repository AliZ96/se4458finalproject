using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SE4458FinalProject.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string PositionLevel { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public int ApplicationCount { get; set; }
        public DateTime LastUpdated { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
} 