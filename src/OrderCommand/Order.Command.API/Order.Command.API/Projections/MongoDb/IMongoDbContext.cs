using MongoDB.Driver;

namespace Order.Command.API.Projections.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoCollection<OrderMaterializedView> OrderMaterializedView();
    }
}
