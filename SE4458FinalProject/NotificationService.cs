using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SE4458FinalProject.Models;

namespace SE4458FinalProject
{
    public class NotificationService
    {
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _notificationsCollection = mongoDatabase.GetCollection<Notification>(mongoDbSettings.Value.NotificationsCollectionName);
        }

        public async Task<List<Notification>> GetAsync(string userId) =>
            await _notificationsCollection.Find(n => n.UserId == userId).ToListAsync();

        public async Task<Notification?> GetByIdAsync(string id) =>
            await _notificationsCollection.Find(n => n.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Notification notification)
        {
            await _notificationsCollection.InsertOneAsync(notification);
        }

        public async Task RemoveAsync(string id) =>
            await _notificationsCollection.DeleteOneAsync(n => n.Id == id);
    }
} 