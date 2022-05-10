using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Image.Models;

namespace SM.Application.Contract.Image
{
    public interface IImageApplication
    {
        OperationResult Create(CreateImage img);
        OperationResult Edit(EditImage img);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditImage GetImage(long id);
        List<ImageViewModel> Search(ImageSearchModel img);
    }
}