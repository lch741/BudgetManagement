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

        public async Task<Transaction?> DeleteAsync(AppUser user,int Id)
        {
            var TransactionModel = await _context.Transactions.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
            if (TransactionModel == null)
            {
                return null;
            }
            _context.Transactions.Remove(TransactionModel);
            await _context.SaveChangesAsync();
            return TransactionModel;
        }

        public async Task<List<Transaction>> GetAllAsync(AppUser user,TransactionQueryObject TransactionQueryObject)
        {
            var Transactions = _context.Transactions.Where(u=>u.AppUserId==user.Id).AsQueryable();
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

        public async Task<Transaction?> GetByIdAsync(AppUser user,int Id)
        {
            var TransactionModel = await _context.Transactions.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
            if (TransactionModel == null)
            {
                return null;
            }
            return TransactionModel;
        }

        public async Task<Transaction?> UpdateAsync(AppUser user,int Id,UpdateTransactionDto UpdateTransactionDto)
        {
            var TransactionModel = await _context.Transactions.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
            if (TransactionModel == null)
            {
                return null;
            }
            TransactionModel.Name = UpdateTransactionDto.Name;
            TransactionModel.Date = UpdateTransactionDto.Date;
            TransactionModel.Amount = UpdateTransactionDto.Amount;
            TransactionModel.CategoryId = UpdateTransactionDto.CategoryId;
            TransactionModel.TransactionType = UpdateTransactionDto.TransactionType;
            await _context.SaveChangesAsync();
            return TransactionModel;
        }
    }
}