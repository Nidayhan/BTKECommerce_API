using BTKECommerce_Core.Services.Abstract;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTKECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        
            private readonly IBasketService _basketService;
            protected Options options;
            public PaymentController(IBasketService basketService)
            {
                options = new Options();
                options.ApiKey = "sandbox-xN4C7fAhmUy5XaUfH50xGxdehEL9qIDk";
                options.SecretKey = "sandbox-UTXNGRZrLmxgayBbGXDEH1t9e3rxdZg2";
                options.BaseUrl = "https://sandbox-api.iyzipay.com";
                _basketService = basketService;
            }

            [HttpPost]
            [Authorize]
            public async Task<IActionResult> CreatePayment(CardInformation model)
            {

                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var userBasketItems = await _basketService.GetBasketItemByUserId(userId);
                decimal totalPrice = 0;

                foreach (var item in userBasketItems.Data.Items)
                {
                    totalPrice = totalPrice + item.Product.Price * item.Quantity;

                }


                CreatePaymentRequest request = new CreatePaymentRequest();
                request.Locale = Locale.TR.ToString();
                request.ConversationId = "123456789";
                request.Price = totalPrice.ToString("0.0");
                request.PaidPrice = totalPrice.ToString("0.0");
                request.Currency = Currency.TRY.ToString();
                request.Installment = 1;
                request.BasketId = "B67832";
                request.PaymentChannel = PaymentChannel.WEB.ToString();
                request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

                PaymentCard paymentCard = new PaymentCard();
                paymentCard.CardHolderName = model.CardHolderName;
                paymentCard.CardNumber = model.CardNumber;
                paymentCard.ExpireMonth = model.ExpireMonth;
                paymentCard.ExpireYear = model.ExpireYear;
                paymentCard.Cvc = model.CVV;
                paymentCard.RegisterCard = 0;
                request.PaymentCard = paymentCard;

                Buyer buyer = new Buyer();
                buyer.Id = "BY789";
                buyer.Name = "John";
                buyer.Surname = "Doe";
                buyer.GsmNumber = "+905350000000";
                buyer.Email = "email@email.com";
                buyer.IdentityNumber = "74300864791";
                buyer.LastLoginDate = "2015-10-05 12:43:35";
                buyer.RegistrationDate = "2013-04-21 15:12:09";
                buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                buyer.Ip = "85.34.78.112";
                buyer.City = "Istanbul";
                buyer.Country = "Turkey";
                buyer.ZipCode = "34732";
                request.Buyer = buyer;

                Address shippingAddress = new Address();
                shippingAddress.ContactName = "Jane Doe";
                shippingAddress.City = "Istanbul";
                shippingAddress.Country = "Turkey";
                shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                shippingAddress.ZipCode = "34742";
                request.ShippingAddress = shippingAddress;

                Address billingAddress = new Address();
                billingAddress.ContactName = "Jane Doe";
                billingAddress.City = "Istanbul";
                billingAddress.Country = "Turkey";
                billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                billingAddress.ZipCode = "34742";
                request.BillingAddress = billingAddress;

                List<Iyzipay.Model.BasketItem> basketItems = new List<Iyzipay.Model.BasketItem>();

                foreach (var item in userBasketItems.Data.Items)
                {
                    Iyzipay.Model.BasketItem firstBasketItem = new Iyzipay.Model.BasketItem();
                    firstBasketItem.Id = item.ProductId.ToString();
                    firstBasketItem.Name = item.Product.ProductName;
                    firstBasketItem.Category1 = item.Product.CategoryId.ToString();
                    firstBasketItem.Category2 = "Accessories";
                    firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                    firstBasketItem.Price = (item.Product.Price * item.Quantity).ToString("0.0");
                    basketItems.Add(firstBasketItem);
                }

                request.BasketItems = basketItems;

                Payment payment = await Payment.Create(request, options);

                return Ok(true);

            }


     }
        //cardHolderName:"",
        //    cardNumber:"",
        //    expireMonth:"",
        //    expireYear:"",
        //    cvv:""

        public class CardInformation
        {
            public string CVV { get; set; }
            public string ExpireYear { get; set; }
            public string ExpireMonth { get; set; }
            public string CardNumber { get; set; }

            public string CardHolderName { get; set; }

        }
}

