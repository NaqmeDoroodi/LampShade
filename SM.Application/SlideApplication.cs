using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Slide;
using SM.Application.Contract.Slide.Models;
using SM.Domain.SlideAgg;

namespace SM.Application
{
    public class SlideApplication : ISlideApplication
    {
        #region inj

        private readonly ISlideRepository _repository;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        #endregion


        public OperationResult Create(CreateSlide slide)
        {
            var operation = new OperationResult();
            var fileName = _fileUploader.Upload(slide.Img, "slides");

            var newSlide = new Slide(fileName, slide.ImgAlt, slide.ImgTitle, slide.Heading, slide.Title, slide.Text,
                slide.BtnText, slide.Link);

            _repository.Add(newSlide);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide slide)
        {
            var operation = new OperationResult();
            var slideToEdit = _repository.Get(slide.Id);
            var fileName = _fileUploader.Upload(slide.Img, "slides");

            if (slideToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slideToEdit.Edit(fileName, slide.ImgAlt, slide.ImgTitle, slide.Heading, slide.Title, slide.Text,
                slide.BtnText, slide.Link);

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Remove();

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Restore();

            _repository.Save();
            return operation.Succeeded();
        }

        public EditSlide GetSlide(long id)
        {
            return _repository.GetSlide(id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _repository.GetSlides();
        }
    }
}
