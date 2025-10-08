using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.Checkout;
using Stripe.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IProductRepository _productRepository;

        public CheckOutService(ICartRepository cartRepository, IOrderRepository orderRepository,IEmailSender emailSender,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrder(orderId);
            var subject = "";
            var body = "";
           
            if (order.PaymentMethod == PaymentMethodEnum.Visa) 
            {
                var productUpdated = new List<(int productId, int quantity)>();
                //here geht foreach jajajjaja


                //inside foreach
                //mov 18 54:06
               // productUpdated.Add((cartItem.ProductId, cartItem.Count));
                
                
                
                
                //here should be contin
                //err here bc no OrderItem !
                await _productRepository.DecreaseQuantityAsync(productUpdated);
                await _cartRepository.ClearCartAsync(order.UserId);
                subject = "Payment Successful - kashop";
                body = $"<h1>thank you</h1>";

            }
            else if (order.PaymentMethod == PaymentMethodEnum.Cash) 
            {
                subject = "Order Successful - kashop";
                body = $"<h1>thank you for ordering</h1>";


            }


            await _emailSender.SendEmailAsync(order.User.Email,subject, body);
            return true;
        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems = _cartRepository.GetUserCart(UserId);
            
            if (!cartItems.Any())
            { 
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is Empty"
                };
            }

            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod=request.PaymentMethod,
                TotalAmount= cartItems.Sum(ci=>ci.Product.Price * ci.Count)


            };

            await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash"
                };

            } 

           
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
               
            },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };


                foreach(var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                        Name = item.Product.Name,
                                        Description =item.Product.Description,
                                    },
                                    UnitAmount = (long)item.Product.Price,
                                },
                                Quantity = item.Count,
                            
                        
                    });


                }
                var service = new SessionService();
                var session = service.Create(options);
                
                order.PaymentId=session.Id;
                return new CheckOutResponse { 
                Success = true,
                Message = "Payment session created successfully",
                PaymentId= session.Id,
                Url = session.Url
                
                };
            }


            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid"
            };

        }
    }
}
