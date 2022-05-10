using System.Collections.Generic;
using Framework.Application;
using Framework.Application.Email;
using Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;
using SM.Domain.OrderAgg;
using SM.Domain.Services;

namespace SM.Application
{
    public class OrderApplication : IOrderApplication
    {
        #region inj

        private readonly ISmsService _msService;
        private readonly IAuthHelper _authHelper;
        private readonly IEmailService _emailService;
        private readonly IOrderRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IShopAccountAcl _shopAccountAcl;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        public OrderApplication(IOrderRepository repository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryAcl shopInventoryAcl, ISmsService msService, IShopAccountAcl shopAccountAcl, IEmailService emailService)
        {
            _repository = repository;
            _authHelper = authHelper;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
            _msService = msService;
            _shopAccountAcl = shopAccountAcl;
            _emailService = emailService;
        }

        #endregion

        public double GetAmountBy(long id)
        {
            return _repository.GetAmountBy(id);
        }

        public long PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.AccountId();
            var order = new Order(accountId,cart.PaymentMethodId, cart.TotalPrice, cart.DiscountPrice, cart.PayPrice);

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem(item.Id, item.UnitePrice, item.DiscRate, item.Count);
                order.Add(orderItem);
            }

            _repository.Add(order);
            _repository.Save();
            return order.Id;
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _repository.Get(orderId);
            var symbol = _configuration.GetSection("TrackingNumberSymbol").Value;
            var trackingNum = CodeGenerator.Generate(symbol);

            if (!_shopInventoryAcl.ReduceFromInventory(order.Items)) return "";

            order.SetTrackingNum(trackingNum);
            order.Succeeded(refId);
            _repository.Save();

            var (name, mobile) = _shopAccountAcl.GetAccountBy(order.AccountId);
            //_msService.Send(mobile,$"{name} عزیز، سفارش شما با کد پیگیری {trackingNum} با موفقیت ثبت شد");
            //_emailService.SendEmail("Hey","DCodeZzZ...","naqmedi@gmail.com");

            return trackingNum;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            return _repository.GetItems(orderId);
        }

        public void Cancel(long id)
        {
            var order = _repository.Get(id);
            order.Cancel();
            _repository.Save();
        }
    }
}
