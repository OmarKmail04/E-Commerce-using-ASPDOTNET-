using KEShop_Api_N_Tier_Art.DAL.Models;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
       // int Add(Cart cart);
        int Add(Cart newItem);
        List<Cart> GetUserCart(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
