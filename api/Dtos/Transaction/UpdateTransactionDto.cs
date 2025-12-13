using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Transaction
{
    public class UpdateTransactionDto
    {
        public required string Name { get; set; }
        public DateOnly Date { get; set; }
        public required decimal Amount { get; set; }
        public required int CategoryId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}