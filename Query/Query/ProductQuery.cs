using System;
using System.Collections.Generic;
using System.Linq;
using CM.Infrastructure.EFCore;
using DM.Infrastructure.EFCore;
using Framework.Application;
using IM.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Comment;
using Query.Contracts.Product;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;
using SM.Domain.ImageAgg;
using SM.Infrastructure.EFCore;

namespace Query.Query
{
    public class ProductQuery : IProductQuery
    {
        #region inj

        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext,
            DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        #endregion

        public ProductQueryModel GetProduct(string slug)
        {
            var inventoryList = _inventoryContext.Inventory
                .Select(x => new {x.ProductId, x.UnitePrice, x.IsInStock}).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscRate, x.EndDate}).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.Images)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Code = product.Code,
                    Img = product.Img,
                    ImgAlt = product.ImgAlt,
                    ImgTitle = product.ImgTitle,
                    ShortDesc = product.ShortDesc,
                    Desc = product.Desc,
                    Category = product.Category.Name,
                    CategorySlug = product.Category.Slug,
                    Slug = product.Slug,
                    Keywords = product.Keywords,
                    MetaDesc = product.MetaDesc,
                    Images = MapImages(product.Images),
                }).FirstOrDefault(x => x.Slug == slug);

            if (product == null) return new ProductQueryModel();


            var inventory = inventoryList.FirstOrDefault(x => x.ProductId == product.Id);
            if (inventory != null)
            {
                product.IsInStock = inventory.IsInStock;

                product.Price = inventory.UnitePrice.ToMoney();
                product.PriceDouble = inventory.UnitePrice;

                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {

                    product.EndDate = discount.EndDate.ToDiscountFormat();
                    var discRate = discount.DiscRate;
                    product.DiscRate = discRate;
                    product.HasDisc = discRate > 0;

                    var discAmount = Math.Round((inventory.UnitePrice * discRate) / 100);
                    product.PriceWithDisc = (inventory.UnitePrice - discAmount).ToMoney();
                    product.PriceWithDiscDouble = (inventory.UnitePrice - discAmount);
                }
            }


            product.Comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.OwnerType == CommentType.Product)
                .Where(x => x.OwnerId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        public static List<ImageQueryModel> MapImages(List<Image> images)
        {
            return images.Select(x => new ImageQueryModel
            {
                IsDeleted = x.IsDeleted,
                ProductId = x.ProductId,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
            }).Where(x => !x.IsDeleted).ToList();
        }

        public List<ProductQueryModel> GetLatestProducts()
        {
            var inventoryList = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitePrice}).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscRate, x.EndDate}).ToList();
            var products = _shopContext.Products.Include(x => x.Category).Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Img = product.Img,
                ImgAlt = product.ImgAlt,
                ImgTitle = product.ImgTitle,
                Category = product.Category.Name,
                Slug = product.Slug,
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();


            foreach (var product in products)
            {
                var inventory = inventoryList.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventory != null)
                    product.Price = inventory.UnitePrice.ToMoney();

                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount == null) continue;
                product.EndDate = discount.EndDate.ToDiscountFormat();

                var discRate = discount.DiscRate;
                product.DiscRate = discRate;
                product.HasDisc = discRate > 0;

                var discAmount = Math.Round((inventory.UnitePrice * discRate) / 100);
                product.PriceWithDisc = (inventory.UnitePrice - discAmount).ToMoney();
            }


            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory.ToList();

            foreach (var item in cartItems)
            {
                if (inventory.Any(x => x.ProductId == item.Id && x.IsInStock))
                {
                    var productInventory = inventory.Find(x => x.ProductId == item.Id);
                    item.IsInStock = productInventory.CalcCurrentCnt() > item.Count;
                }
            }

            return cartItems;
        }

        public List<ProductQueryModel> Search(string searchString)
        {
            var inventoryList = _inventoryContext.Inventory.Select(x => new {x.ProductId, x.UnitePrice}).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscRate, x.EndDate}).ToList();
            var query = _shopContext.Products.Include(x => x.Category).Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Img = product.Img,
                ImgAlt = product.ImgAlt,
                ImgTitle = product.ImgTitle,
                Category = product.Category.Name,
                CategorySlug = product.Category.Slug,
                ShortDesc = product.ShortDesc,
                Slug = product.Slug,
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(x => x.Name.Contains(searchString) || x.ShortDesc.Contains(searchString));


            var products = query.OrderByDescending(x => x.Id).ToList();


            foreach (var product in products)
            {
                var inventory = inventoryList.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventory != null)
                    product.Price = inventory.UnitePrice.ToMoney();

                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount == null) continue;
                product.EndDate = discount.EndDate.ToDiscountFormat();

                var discRate = discount.DiscRate;
                product.DiscRate = discRate;
                product.HasDisc = discRate > 0;

                var discAmount = Math.Round((inventory.UnitePrice * discRate) / 100);
                product.PriceWithDisc = (inventory.UnitePrice - discAmount).ToMoney();
            }


            return products;
        }
    }
}