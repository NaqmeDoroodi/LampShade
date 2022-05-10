namespace DM.Application.Contracts.Colleague.Models
{
    public class ColleagueDiscViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscRate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationDate { get; set; }
    }
}