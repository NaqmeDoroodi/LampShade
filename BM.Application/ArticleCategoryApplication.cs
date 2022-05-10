using System.Collections.Generic;
using BM.Application.Contract.Category;
using BM.Application.Contract.Category.Models;
using BM.Domain.CategoryAgg;
using Framework.Application;

namespace BM.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        #region inj

        private readonly IArticleCategoryRepository _repository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        #endregion

        public OperationResult Create(CreateArticleCategory category)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Name == category.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = category.Slug.Slugify();
            var fileName = _fileUploader.Upload(category.Img, slug);
            var newCategory = new ArticleCategory(category.Name, fileName, category.ImgAlt, category.ImgTitle, category.Desc, category.ShowOrder, slug,
                category.Keywords, category.MetaDesc, category.CanonicalAddress);

            _repository.Add(newCategory);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory category)
        {
            var operation = new OperationResult();
            var categoryToEdit = _repository.Get(category.Id);

            if (categoryToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.Name == category.Name && x.Id != category.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = category.Slug.Slugify();
            var fileName = _fileUploader.Upload(category.Img, slug);
            categoryToEdit.Edit(category.Name, fileName, category.ImgAlt, category.ImgTitle, category.Desc, category.ShowOrder, slug,
                category.Keywords, category.MetaDesc, category.CanonicalAddress);
            
            _repository.Save();
            return operation.Succeeded();
        }

        public EditArticleCategory GetCategoryDetails(long id)
        {
            return _repository.GetCategoryDetails(id);
        }

        public List<ArticleCategoryViewModel> GetCategories()
        {
            return _repository.GetCategories();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}