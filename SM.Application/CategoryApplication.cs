using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Category;
using SM.Application.Contract.Category.Models;
using SM.Domain.CategoryAgg;

namespace SM.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        #region inj

        private readonly ICategoryRepository _repository;
        private readonly IFileUploader _fileUploader;

        public CategoryApplication(ICategoryRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        #endregion

        public List<CategoryViewModel> GetCategories()
        {
            return _repository.GetCategories();
        }

        public string GetSlugBy(long id)
        {
            return _repository.GetCategorySlug(id);
        }

        public List<CategoryViewModel> Search(CategorySearchModel category)
        {
            return _repository.Search(category);
        }

        public EditCategory GetCategory(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Create(CreateCategory category)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Name == category.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = category.Name.Slugify();
            var fileName = _fileUploader.Upload(category.Img, category.Slug);

            var newCategory = new Category(category.Name, category.Desc, fileName, category.ImgAlt,
                category.ImgTitle, category.Keywords, category.MetaDesc, slug);
            _repository.Add(newCategory);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCategory category)
        {
            var operation = new OperationResult();
            var categoryToEdit = _repository.Get(category.Id);

            if (categoryToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if(_repository.DoesExist(x=>x.Name == category.Name && x.Id != category.Id))
               return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = category.Slug.Slugify();
            var fileName = _fileUploader.Upload(category.Img, category.Slug);

            categoryToEdit.Edit(category.Name, category.Desc,fileName, category.ImgAlt,
                category.ImgTitle, category.Keywords, category.MetaDesc, slug);
            _repository.Save();
            return operation.Succeeded();
        }
    }
}