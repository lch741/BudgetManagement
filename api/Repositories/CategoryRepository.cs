using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Category;
using api.Dtos.Transaction;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api.Repositories
{
    public class CategoryRepository(ApplicationDBContext context) : ICategoryRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<Category> CreateAsync(Category Category)
        {
            await _context.Categorys.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Category;
        }
        public async Task<Category?> DeleteAsync(AppUser user,int Id)
        {
            var CategoryModel = await _context.Categorys.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
            if (CategoryModel==null)
            {
                return null;
            }
           _context.Categorys.Remove(CategoryModel);
           await _context.SaveChangesAsync();
           return CategoryModel;
        }

        public Task<List<Category>> GetAllAsync(AppUser user)
        {
            return _context.Categorys.Where(u=>u.AppUserId==user.Id).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(AppUser user,int Id)
        {
            return await _context.Categorys.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
        }

        public async Task<Category?> UpdateAsync(AppUser user,int Id,UpdateCategoryDto UpdateCategoryDto)
        {
            var CategoryModel = await _context.Categorys.FirstOrDefaultAsync(u=>u.AppUserId==user.Id&&u.Id==Id);
            if (CategoryModel == null)
            {
                return null;
            }
            CategoryModel.Name = UpdateCategoryDto.Name;
            await _context.SaveChangesAsync();
            return CategoryModel;
        }
    }
}