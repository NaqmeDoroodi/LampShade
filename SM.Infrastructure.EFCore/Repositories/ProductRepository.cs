using System.Collections.Generic;
using System.Linq;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contract.Product.Models;
using SM.Domain.ProductAgg;

namespace SM.Infrastructure.EFCore.Repositories
{
    public class ProductRepository : BaseRepository<long, Product>, IProductRepository
    {
        #region inj

        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditProduct GetProduct(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                ShortDesc = x.ShortDesc,
                Desc = x.Desc,
                MetaDesc = x.MetaDesc,
                Slug = x.Slug,
                Keywords = x.Keywords,
                CategoryId = x.CategoryId
            }).FirstOrDefault(x => x.Id == id);
        }

        public Product GetWithCategory(long id)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel product)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Code = x.Code,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Id = x.Id,
                    Img = x.Img,
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if (!string.IsNullOrWhiteSpace(product.Name))
                query = query.Where(x => x.Name.Contains(product.Name));

            if (!string.IsNullOrWhiteSpace(product.Code))
                query = query.Where(x => x.Code.Contains(product.Code));

            if (product.CategoryId != 0)
                query = query.Where(x => x.CategoryId == product.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}