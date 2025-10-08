
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplictionDbContext _context;

        public CartRepository(ApplictionDbContext context)
        {
            _context = context;
        }
        public int Add(Cart cart)
        {
            _context.Carts.Add(cart);
            return _context.SaveChanges();
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            var items = _context.Carts.Where(c=>c.UserId==UserId).ToList();

            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;

        }

        public List<Cart> GetUserCart(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
