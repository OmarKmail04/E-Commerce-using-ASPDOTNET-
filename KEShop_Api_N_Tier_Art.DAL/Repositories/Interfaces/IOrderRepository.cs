using KEShop_Api_N_Tier_Art.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrder(int orderId);
        Task<Order?> AddAsync(Order orderId);

        Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum newStatus);

        Task<List<Order>> GetOrderByUserAsync(string userId);
        Task<List<Order>> GetByStatusAsync(OrderStatusEnum status);
        Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId);

    }
}
