using System;
using System.Collections.Generic;
using System.Linq;
using DM.Infrastructure.EFCore;
using Framework.Application;
using IM.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Category;
using Query.Contracts.Product;
using SM.Domain.ProductAgg;
using SM.Infrastructure.EFCore;

namespace Query.Query
{
    public class CategoryQuery : ICategoryQuery
    {
        #region inj

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public CategoryQuery(ShopContext shopContext, InventoryContext inventoryContext,
            DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        #endregion


        public List<CategoryQueryModel> GetCategoriesForMenu()
        {
            return _shopContext.Categories.Select(x=>new CategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug,
                Products = x.Products.Select(x => new ProductQueryModel { Name = x.Name, Slug = x.Slug }).ToList(),
            }).ToList();
        }

        public List<CategoryQueryModel> GetCategories()
        {
            return _shopContext.Categories.Select(x => new CategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Slug = x.Slug,
            }).AsNoTracking().ToList();
        }

        public List<CategoryQueryModel> GetCategoriesWithProducts()
        {
            var inventoryList = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitePrice}).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new {x.ProductId, x.DiscRate}).ToList();

            var categories = _shopContext.Categories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new CategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products =MapProducts(x.Products),
                }).AsNoTracking().ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var inventory = inventoryList.FirstOrDefault(x => x.ProductId == product.Id);
                    if (inventory != null)
                        product.Price = inventory.UnitePrice.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount == null) continue;

                    var discRate = discount.DiscRate;
                    product.DiscRate = discRate;
                    product.HasDisc = discRate > 0;

                    var discAmount = Math.Round((inventory.UnitePrice * discRate) / 100);
                    product.PriceWithDisc = (inventory.UnitePrice - discAmount).ToMoney();
                }
            }

            return categories;
        }

        public CategoryQueryModel GetCategoryWithProducts(string slug)
        {
            var inventoryList = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitePrice}).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new {x.ProductId, x.DiscRate, x.EndDate}).ToList();

            var category = _shopContext.Categories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new CategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Desc = x.Desc,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDesc = x.MetaDesc,
                    Products = MapProducts(x.Products),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);


            foreach (var product in category.Products)
            {
                var inventory = inventoryList.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventory != null) product.Price = inventory.UnitePrice.ToMoney();

                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount == null) continue;

                var discRate = discount.DiscRate;
                product.DiscRate = discRate;
                product.HasDisc = discRate > 0;
                product.EndDate = discount.EndDate.ToDiscountFormat();

                var discAmount = Math.Round((inventory.UnitePrice * discRate) / 100);
                product.PriceWithDisc = (inventory.UnitePrice - discAmount).ToMoney();
            }

            return category;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Category = x.Category.Name,
                Slug = x.Slug,
            }).ToList();
        }
    }
}