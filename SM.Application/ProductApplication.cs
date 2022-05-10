using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Product;
using SM.Application.Contract.Product.Models;
using SM.Domain.CategoryAgg;
using SM.Domain.ProductAgg;

namespace SM.Application
{
    public class ProductApplication : IProductApplication
    {
        #region inj

        private readonly IProductRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly ICategoryRepository _categoryRepository;

        public ProductApplication(IProductRepository repository, IFileUploader fileUploader,
            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public OperationResult Create(CreateProduct product)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Name == product.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = product.Slug.Slugify();
            var path = $"{_categoryRepository.GetCategorySlug(product.CategoryId)}//{slug}";
            var imagePath = _fileUploader.Upload(product.Img, path);

            var newProduct = new Product(product.Name, product.Code, imagePath, product.ImgAlt,
                product.ImgTitle, product.ShortDesc, product.MetaDesc, product.MetaDesc, slug, product.Keywords,
                product.CategoryId);

            _repository.Add(newProduct);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditProduct GetProduct(long id)
        {
            return _repository.GetProduct(id);
        }

        public OperationResult Edit(EditProduct product)
        {
            var operation = new OperationResult();
            var editProduct = _repository.GetWithCategory(product.Id);

            if (editProduct == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.Name == product.Name && x.Id != product.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = editProduct.Slug.Slugify();
            var path = $"{editProduct.Category.Slug}//{slug}";
            var imagePath = _fileUploader.Upload(product.Img, path);

            editProduct.Edit(product.Name, product.Code, imagePath, product.ImgAlt,
                product.ImgTitle, product.ShortDesc, product.MetaDesc, product.MetaDesc, slug, product.Keywords,
                product.CategoryId);
            _repository.Save();
            return operation.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel product)
        {
            return _repository.Search(product);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _repository.GetProducts();
        }
    }
}