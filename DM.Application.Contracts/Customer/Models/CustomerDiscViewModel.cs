using System;

namespace DM.Application.Contracts.Customer.Models
{
    public class CustomerDiscViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscRate { get; set; }
        public string StartDateStr { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDateStr { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
    }
}