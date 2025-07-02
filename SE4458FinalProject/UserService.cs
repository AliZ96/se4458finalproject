using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SE4458FinalProject.Models;

namespace SE4458FinalProject
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(mongoDbSettings.Value.UsersCollectionName);
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _usersCollection.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<User?> GetByIdAsync(string id) =>
            await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }
    }
} 