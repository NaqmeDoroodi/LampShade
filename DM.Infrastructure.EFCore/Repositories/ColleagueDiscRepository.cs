using System.Collections.Generic;
using System.Linq;
using DM.Application.Contracts.Colleague.Models;
using DM.Domain.ColleagueAgg;
using Framework.Application;
using Framework.Infrastructure;
using SM.Infrastructure.EFCore;

namespace DM.Infrastructure.EFCore.Repositories
{
    public class ColleagueDiscRepository : BaseRepository<long,ColleagueDisc>, IColleagueDiscRepository
    {
        #region inj

        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public ColleagueDiscRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        #endregion

        public EditColleagueDisc GetDiscDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new EditColleagueDisc
                {
                    Id = x.Id,
                    DiscRate = x.DiscRate,
                    ProductId = x.ProductId,
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscViewModel> Search(ColleagueDiscSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscViewModel
            {
                Id = x.Id,
                DiscRate = x.DiscRate,
                IsDeleted = x.IsDeleted,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
            });


            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(d=> d.Product = products.FirstOrDefault(x=> x.Id == d.Id)?.Name);
            return discounts;
        }
    }
}
