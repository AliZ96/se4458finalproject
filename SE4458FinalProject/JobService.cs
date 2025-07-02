using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SE4458FinalProject.Models;
using MongoDB.Bson;

namespace SE4458FinalProject
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string JobsCollectionName { get; set; } = string.Empty;
        public string UsersCollectionName { get; set; } = string.Empty;
        public string NotificationsCollectionName { get; set; } = string.Empty;
    }

    public class JobService
    {
        private readonly IMongoCollection<Job> _jobsCollection;

        public JobService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _jobsCollection = mongoDatabase.GetCollection<Job>(mongoDbSettings.Value.JobsCollectionName);
        }

        public async Task<List<Job>> GetAsync() =>
            await _jobsCollection.Find(_ => true).ToListAsync();

        public async Task<Job?> GetAsync(string id) =>
            await _jobsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Job job)
        {
            await _jobsCollection.InsertOneAsync(job);
        }

        public async Task UpdateAsync(string id, Job updatedJob) =>
            await _jobsCollection.ReplaceOneAsync(x => x.Id == id, updatedJob);

        public async Task RemoveAsync(string id) =>
            await _jobsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Job>> SearchAsync(string? title, string? company, string? country, string? city, string? town, string? department, string? positionLevel, string? workType)
        {
            var filterBuilder = Builders<Job>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(title))
                filter &= filterBuilder.Regex(j => j.Title, new MongoDB.Bson.BsonRegularExpression(title, "i"));
            if (!string.IsNullOrEmpty(company))
                filter &= filterBuilder.Regex(j => j.Company, new MongoDB.Bson.BsonRegularExpression(company, "i"));
            if (!string.IsNullOrEmpty(country))
                filter &= filterBuilder.Regex(j => j.Country, new MongoDB.Bson.BsonRegularExpression(country, "i"));
            if (!string.IsNullOrEmpty(city))
                filter &= filterBuilder.Regex(j => j.City, new MongoDB.Bson.BsonRegularExpression(city, "i"));
            if (!string.IsNullOrEmpty(town))
                filter &= filterBuilder.Regex(j => j.Town, new MongoDB.Bson.BsonRegularExpression(town, "i"));
            if (!string.IsNullOrEmpty(department))
                filter &= filterBuilder.Regex(j => j.Department, new MongoDB.Bson.BsonRegularExpression(department, "i"));
            if (!string.IsNullOrEmpty(positionLevel))
                filter &= filterBuilder.Regex(j => j.PositionLevel, new MongoDB.Bson.BsonRegularExpression(positionLevel, "i"));
            if (!string.IsNullOrEmpty(workType))
                filter &= filterBuilder.Regex(j => j.WorkType, new MongoDB.Bson.BsonRegularExpression(workType, "i"));

            return await _jobsCollection.Find(filter).ToListAsync();
        }

        public async Task<bool> ApplyAsync(string id)
        {
            var filter = Builders<Job>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<Job>.Update.Inc(j => j.ApplicationCount, 1);
            var result = await _jobsCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
} 