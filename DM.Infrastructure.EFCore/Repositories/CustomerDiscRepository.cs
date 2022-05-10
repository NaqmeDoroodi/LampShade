using System.Collections.Generic;
using System.Linq;
using DM.Application.Contracts.Customer.Models;
using DM.Domain.CustomerAgg;
using Framework.Application;
using Framework.Infrastructure;
using SM.Infrastructure.EFCore;

namespace DM.Infrastructure.EFCore.Repositories
{
    public class CustomerDiscRepository : BaseRepository<long, CustomerDisc>, ICustomerDiscRepository
    {
        #region inj

        private readonly DiscountContext _discContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscRepository(DiscountContext discContext, ShopContext shopContext) : base(discContext)
        {
            _discContext = discContext;
            _shopContext = shopContext;
        }

        #endregion


        public EditCustomerDisc GetDiscDetails(long id)
        {
            return _discContext.CustomerDiscounts.Select(x => new EditCustomerDisc
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscRate = x.DiscRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                Reason = x.Reason,
            }).FirstOrDefault(x => x.Id == id);
        }


        public List<CustomerDiscViewModel> Search(CustomerDiscSearchModel disc)
        {
            var products = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
            var query = _discContext.CustomerDiscounts.Select(x => new CustomerDiscViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscRate = x.DiscRate,
                StartDate = x.StartDate,
                StartDateStr = x.StartDate.ToFarsi(), 
                EndDate = x.EndDate,
                EndDateStr = x.EndDate.ToFarsi(),
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if (disc.ProductId > 0)
                query = query.Where(x => x.ProductId == disc.ProductId);

            if (!string.IsNullOrWhiteSpace(disc.StartDate))
                query = query.Where(x => x.StartDate >= disc.StartDate.ToGeorgianDateTime());


            if (!string.IsNullOrWhiteSpace(disc.EndDate))
                query = query.Where(x => x.EndDate <= disc.EndDate.ToGeorgianDateTime());


            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(d=> d.Product = products.FirstOrDefault(x=> x.Id == d.Id)?.Name);
            return discounts;
        }
    }
}