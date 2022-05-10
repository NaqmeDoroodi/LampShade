using CM.Application.Contract.Comment;
using CM.Application.Contract.Comment.Models;
using CM.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        #region inj

        private readonly IProductQuery _query;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery query, ICommentApplication commentApplication)
        {
            _query = query;
            _commentApplication = commentApplication;
        }

        #endregion

        public ProductQueryModel Product;

        public void OnGet(string id)
        {
            Product = _query.GetProduct(id);
        }


        public IActionResult OnPost(AddComment comment, string productSlug)
        {
            comment.OwnerType = CommentType.Product;
            var result = _commentApplication.Add(comment);
            return RedirectToPage("./Product", new {Id = productSlug});
        }
    }
}