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
        Task<List<Transaction>>GetAllAsync(AppUser user,TransactionQueryObject TransactionQueryObject);
        Task<Transaction?>GetByIdAsync(AppUser user,int Id);
        Task<Transaction>CreateAsync(Transaction Transaction);
        Task<Transaction?>DeleteAsync(AppUser user,int Id);
        Task<Transaction?>UpdateAsync(AppUser user,int Id,UpdateTransactionDto UpdateTransactionDto);
    }
}