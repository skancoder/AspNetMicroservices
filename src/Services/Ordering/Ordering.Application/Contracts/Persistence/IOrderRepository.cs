using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    //with this repository classes, we handle DB related actions and abstracted with creating interfaces.
    //this interfaces implemented in infrastructure layer using EFCore and SQL server DB
    //but this interface can andle any ORM and any DB with no effect in Application layer.only Infarstucture layer changes

    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
