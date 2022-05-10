namespace CM.Application.Contract.Comment.Models
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int OwnerType { get; set; }
        public string CommentDate { get; set; }
    }
}