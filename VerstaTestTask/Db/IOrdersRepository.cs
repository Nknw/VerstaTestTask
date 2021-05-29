using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VerstaTestTask.Db
{
    public interface IOrdersRepository
    {
        IAsyncEnumerable<Order> GetOrdersAsync();
        Task<Order> InsertOrderAsync(Order order);
    }

    public class OrdersRepository : IOrdersRepository
    {
        private readonly EFContext _context;

        public OrdersRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<Order> InsertOrderAsync(Order order)
        {
            var orderEntity = await  _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return orderEntity.Entity;
        }

        public IAsyncEnumerable<Order> GetOrdersAsync()
        {
            return _context.Orders.AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
