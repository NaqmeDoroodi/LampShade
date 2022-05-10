using Framework.Domain;

namespace DM.Domain.ColleagueAgg
{
    public class ColleagueDisc : BaseEntity
    {
        public long ProductId { get; private set; }
        public int DiscRate { get; private set; }
        public bool IsDeleted { get; private set; }


        public ColleagueDisc(long productId, int discRate)
        {
            ProductId = productId;
            DiscRate = discRate;
            IsDeleted = false;
        }


        public void Edit(long productId, int discRate)
        {
            ProductId = productId;
            DiscRate = discRate;
        }


        public void Remove()
        {
            IsDeleted = true;
        }


        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
