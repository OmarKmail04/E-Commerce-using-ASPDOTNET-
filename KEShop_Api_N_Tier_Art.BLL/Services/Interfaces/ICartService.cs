using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;


namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(CartRequest request, string UserId);
        CartSummaryResponse CartSummaryResponse(string UserId);

        Task<bool> ClearCartAsync(string UserId);
    }
}
