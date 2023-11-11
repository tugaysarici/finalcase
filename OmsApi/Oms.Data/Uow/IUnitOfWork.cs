using Oms.Data.Domain;
using Oms.Data.Repository;

namespace Oms.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteTransaction();

    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Chat> ChatRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderItem> OrderItemRepository { get; }
    // ...

}