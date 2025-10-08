using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
 
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public bool AddToCart(CartRequest request, string UserId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
               UserId=UserId,
                Count= 1


            };
            return _cartRepository.Add(newItem) > 0;
        }

        public CartSummaryResponse CartSummaryResponse(string UserId)
        {
            var cartItems = _cartRepository.GetUserCart(UserId);
            var response = new CartSummaryResponse
            {

                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId= ci.ProductId,
                    ProductName= ci.Product.Name,
                    Count = ci.Count,
                    Price = ci.Product.Price

                }).ToList()

            };
            return response;
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
          return await _cartRepository.ClearCartAsync(UserId);
        }
    }
}
