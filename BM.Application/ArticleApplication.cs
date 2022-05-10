using System;
using System.Collections.Generic;
using BM.Application.Contract.Article;
using BM.Application.Contract.Article.Models;
using BM.Domain.ArticleAgg;
using BM.Domain.CategoryAgg;
using Framework.Application;

namespace BM.Application
{
    public class ArticleApplication : IArticleApplication
    {
        #region inj

        private readonly IArticleRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _categoryRepository;

        public ArticleApplication(IArticleRepository repository, IFileUploader fileUploader,
            IArticleCategoryRepository categoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public OperationResult Create(CreateArticle article)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.Title == article.Title))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = article.Slug.Slugify();
            var path = $"{_categoryRepository.GetSlugBy(article.CategoryId)}//{slug}";
            var fileName = _fileUploader.Upload(article.Img, path);
            var publishDate = article.PublishDate.ToGeorgianDateTime();
            var newArticle = new Article(article.Title, article.ShortDesc, article.Desc, fileName, article.ImgAlt,
                article.ImgTitle, publishDate, slug, article.MetaDesc, article.Keywords, article.CanonicalAddress,
                article.CategoryId);

            _repository.Add(newArticle);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle article)
        {
            var operation = new OperationResult();
            var articleToEdit = _repository.GetWithCategory(article.Id);

            if (articleToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.Title == article.Title && x.Id != article.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = article.Slug.Slugify();
            var path = $"{articleToEdit.Category.Slug}//{slug}";
            var fileName = _fileUploader.Upload(article.Img, path);
            var publishDate = article.PublishDate.ToGeorgianDateTime();
            articleToEdit.Edit(article.Title, article.ShortDesc, article.Desc, fileName, article.ImgAlt,
                article.ImgTitle, publishDate, slug, article.MetaDesc, article.Keywords, article.CanonicalAddress,
                article.CategoryId);

            _repository.Save();
            return operation.Succeeded();
        }

        public EditArticle GetArticleDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}