using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class TransactionQueryObject
    {
        public string? Name { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int? CategoryId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public bool? IsDescendingBydate  { get; set; }
        public bool? IsDescendingByamount { get; set; }
    }
}
