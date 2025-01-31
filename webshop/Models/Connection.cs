using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Models
{
    internal class Connection
    {
        private static async Task<MongoClient> GetClientAsync()
        {
            string connectionString = "mongodb+srv://johanneslindell:pETokehPndCHIVLl@cluster0.xldnv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls13 };

            var client = new MongoClient(settings);

        // MongoClient inte har asynkrona konstruktorer eller initiering, men vi behåller Task för framtida utbyggnad.
            return await Task.FromResult(client);
        }

        public static async Task<IMongoCollection<T>> GetCollectionAsync<T>(string collectionName)
        {
            var client = await GetClientAsync();
            var database = client.GetDatabase("ShoppenDb");

            return database.GetCollection<T>(collectionName);
        }

        public static async Task<IMongoCollection<Log<User>>> GetUserLogsAsync()
        {
            return await GetCollectionAsync<Log<User>>("UserLogs");
        }

        public static async Task<IMongoCollection<Log<AdminUser>>> GetAdminLogsAsync()
        {
            return await GetCollectionAsync<Log<AdminUser>>("AdminLogs");
        }
    }
}
