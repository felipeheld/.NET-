using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.SettingsModels;
using MongoDB.Driver;

namespace Api.Services 
{
    public class DemoService
    {
        private readonly IMongoCollection<Demo> _demo;

        public DemoService(IDemoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _demo = database.GetCollection<Demo>(settings.DemoCollectionName);
        }

        public Task<List<Demo>> Get() =>
            _demo.FindAsync(demo => true)
            .GetAwaiter()
            .GetResult()
            .ToListAsync();

        public Task<Demo> Get(string id) =>
            _demo.FindAsync(demo => demo.Id == id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .FirstOrDefaultAsync();            
        
    }
}