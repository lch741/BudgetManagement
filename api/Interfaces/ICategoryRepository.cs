using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>>GetAllAsync(AppUser user);
        Task<Category?>GetByIdAsync(AppUser user,int Id);
        Task<Category>CreateAsync(Category Category);
        Task<Category?>DeleteAsync(AppUser user,int Id);
        Task<Category?>UpdateAsync(AppUser user,int Id,UpdateCategoryDto UpdateCategoryDto);
    }
}