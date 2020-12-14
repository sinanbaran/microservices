using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Order.Command.API.Projections.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase Db { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDbContext(IConfiguration configuration)
        {
            var mongoConnection = configuration.GetSection("MongoConnection").Get<string>();
            var database = configuration.GetSection("MongoDatabase").Get<string>();
            Console.WriteLine($"Mongo Connection String  {mongoConnection}");
            Console.WriteLine($"Mongo Database  {database}");
            MongoClient = new MongoClient(mongoConnection);
            Db = MongoClient.GetDatabase(database);
        }

        public IMongoCollection<OrderMaterializedView> OrderMaterializedView()
        {
            return Db.GetCollection<OrderMaterializedView>(nameof(Projections.OrderMaterializedView));
        }
    }
}
