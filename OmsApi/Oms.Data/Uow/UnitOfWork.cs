using Serilog;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Data.Repository;

namespace Oms.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly OmsDbContext dbContext;

    public UnitOfWork(OmsDbContext dbContext)
    {
        this.dbContext = dbContext;

        UserRepository = new GenericRepository<User>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
        ChatRepository = new GenericRepository<Chat>(dbContext);
        OrderRepository = new GenericRepository<Order>(dbContext);
        OrderItemRepository = new GenericRepository<OrderItem>(dbContext);
        // ...
    }

    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteTransaction()
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error("CompleteTransactionError", ex);
            }
        }
    }

    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set; }
    public IGenericRepository<Chat> ChatRepository { get; private set; }
    public IGenericRepository<Order> OrderRepository { get; private set; }
    public IGenericRepository<OrderItem> OrderItemRepository { get; private set; }
    // ...
}