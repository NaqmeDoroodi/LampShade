namespace CM.Application.Contract.Comment.Models
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public long OwnerId { get; set; }
        public int OwnerType { get; set; }
        public long ParentId { get; set; }
    }
}