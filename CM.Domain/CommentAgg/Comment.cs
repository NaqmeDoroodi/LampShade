using System.Collections.Generic;
using Framework.Domain;

namespace CM.Domain.CommentAgg
{
    public class Comment : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerId { get; private set; }
        public int OwnerType { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }
       
    
        public Comment(string name, string email, string website, string message, long ownerId, int ownerType, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerId = ownerId;
            OwnerType = ownerType;
            ParentId = parentId;
            IsCanceled = false;
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}