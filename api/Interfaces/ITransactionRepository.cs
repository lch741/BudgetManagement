using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Transaction;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>>GetAllAsync(TransactionQueryObject TransactionQueryObject);
        Task<Transaction?>GetByIdAsync(int Id);
        Task<Transaction>CreateAsync(Transaction Transaction);
        Task<Transaction?>DeleteAsync(int Id);
        Task<Transaction?>UpdateAsync(int Id,UpdateTransactionDto UpdateTransactionDto);
    }
}