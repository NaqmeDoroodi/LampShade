using System.Collections.Generic;
using CM.Application.Contract.Comment;
using CM.Application.Contract.Comment.Models;
using CM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly ICommentApplication _service;

        public IndexModel(ICommentApplication service)
        {
            _service = service;
        }

        #endregion

        public List<CommentViewModel> Comments { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(CommentPermission.CommentList)]
        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _service.Search(searchModel);
        }


        [NeedsPermission(CommentPermission.CommentCancel)]
        public IActionResult OnGetCancel(long id)
        {
            var result = _service.Cancel(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


        [NeedsPermission(CommentPermission.CommentConfirm)]
        public IActionResult OnGetConfirm(long id)
        {
            var result = _service.Confirm(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}