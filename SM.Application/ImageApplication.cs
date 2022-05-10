using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Image;
using SM.Application.Contract.Image.Models;
using SM.Domain.ImageAgg;
using SM.Domain.ProductAgg;

namespace SM.Application
{
    public class ImageApplication : IImageApplication
    {
        #region inj

        private readonly IImageRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ImageApplication(IImageRepository repository, IProductRepository productRepository,
            IFileUploader fileUploader)
        {
            _repository = repository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        #endregion

        public OperationResult Create(CreateImage img)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetWithCategory(img.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";
            var imagePath = _fileUploader.Upload(img.Img, path);

            var newImg = new Image(imagePath, img.ImgAlt, img.ImgTitle, img.ProductId);

            _repository.Add(newImg);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditImage img)
        {
            var operation = new OperationResult();
            var imgToEdit = _repository.GetWithProductAndCategory(img.Id);

            if (imgToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var path = $"{imgToEdit.Product.Category.Slug}//{imgToEdit.Product.Slug}";
            var imagePath = _fileUploader.Upload(img.Img, path);

            imgToEdit.Edit(imagePath, img.ImgAlt, img.ImgTitle, img.ProductId);

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var img = _repository.Get(id);

            if (img == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            img.Remove();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var img = _repository.Get(id);

            if (img == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            img.Restore();
            _repository.Save();
            return operation.Succeeded();
        }

        public EditImage GetImage(long id)
        {
            return _repository.GetImage(id);
        }

        public List<ImageViewModel> Search(ImageSearchModel img)
        {
            return _repository.Search(img);
        }
    }
}