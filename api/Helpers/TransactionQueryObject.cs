using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class TransactionQueryObject
    {
        public int? Id {get;set;}
        public string? Name { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int? CategoryId { get; set; }
        public DateOnly? Date { get; set; }
        public bool IsDescending  { get; set; } = true;
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}