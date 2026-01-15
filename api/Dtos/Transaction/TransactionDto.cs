using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Date { get; set; }
        public required decimal Amount { get; set; }
        public required string CategoryName { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}