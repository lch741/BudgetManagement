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
        Task<List<Category>>GetAllAsync();
        Task<Category?>GetByIdAsync(int Id);
        Task<Category>CreateAsync(Category Category);
        Task<Category?>DeleteAsync(int Id);
        Task<Category?>UpdateAsync(int Id,UpdateCategoryDto UpdateCategoryDto);
    }
}