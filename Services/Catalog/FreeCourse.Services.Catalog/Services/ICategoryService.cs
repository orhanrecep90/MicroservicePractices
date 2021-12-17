using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
   public interface ICategoryService
    {
        public  Task<Response<List<CategoryDto>>> GetAllAsync();
        public Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        public  Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
