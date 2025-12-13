using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Transaction;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api.Repositories
{
    public class TransactionRepository(ApplicationDBContext context) : ITransactionRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<Transaction> CreateAsync(Transaction Transaction)
        {
            await _context.Transactions.AddAsync(Transaction);
            await _context.SaveChangesAsync();
            return Transaction;
        }

        public async Task<Transaction?> DeleteAsync(int Id)
        {
            var TransactionModel = await _context.Transactions.FindAsync(Id);
            if (TransactionModel == null)
            {
                return null;
            }
            _context.Transactions.Remove(TransactionModel);
            await _context.SaveChangesAsync();
            return TransactionModel;
        }

        public async Task<List<Transaction>> GetAllAsync(TransactionQueryObject TransactionQueryObject)
        {
            var Transactions = _context.Transactions.AsQueryable();
            if (!string.IsNullOrWhiteSpace(TransactionQueryObject.Name))
            {
                Transactions = Transactions.Where(x=>x.Name.Contains(TransactionQueryObject.Name));
            }
            if (TransactionQueryObject.TransactionType!=null)
            {
                Transactions = Transactions.Where(x=>x.TransactionType==TransactionQueryObject.TransactionType);
            }
            if (TransactionQueryObject.CategoryId!=null)
            {
                Transactions = Transactions.Where(x=>x.CategoryId==TransactionQueryObject.CategoryId);
            }
            if (TransactionQueryObject.StartDate!=null)
            {
                Transactions = Transactions.Where(x=>x.Date>=TransactionQueryObject.StartDate);
            }
            if (TransactionQueryObject.EndDate!=null)
            {
                Transactions = Transactions.Where(x=>x.Date<=TransactionQueryObject.EndDate);
            }
            if (TransactionQueryObject.MinAmount!=null)
            {
                Transactions = Transactions.Where(x=>x.Amount>=TransactionQueryObject.MinAmount);
            }
            if (TransactionQueryObject.MaxAmount!=null)
            {
                Transactions = Transactions.Where(x=>x.Amount<=TransactionQueryObject.MaxAmount);
            }
            if (TransactionQueryObject.IsDescendingBydate==true)
            {
                Transactions = Transactions.OrderByDescending(x=>x.Date);
            }
            if (TransactionQueryObject.IsDescendingBydate==false)
            {
                Transactions = Transactions.OrderBy(x=>x.Date);
            }
            if (TransactionQueryObject.IsDescendingByamount==true)
            {
                Transactions = Transactions.OrderByDescending(x=>x.Amount);
            }
            if (TransactionQueryObject.IsDescendingByamount==false)
            {
                Transactions = Transactions.OrderBy(x=>x.Amount);
            }
            return await Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int Id)
        {
            var TransactionModel = await _context.Transactions.FindAsync(Id);
            if (TransactionModel == null)
            {
                return null;
            }
            return TransactionModel;
        }

        public async Task<Transaction?> UpdateAsync(int Id,UpdateTransactionDto UpdateTransactionDto)
        {
            var TransactionModel = await _context.Transactions.FindAsync(Id);
            if (TransactionModel == null)
            {
                return null;
            }
            _context.Entry(TransactionModel).CurrentValues.SetValues(UpdateTransactionDto);
            await _context.SaveChangesAsync();
            return TransactionModel;
        }
    }
}