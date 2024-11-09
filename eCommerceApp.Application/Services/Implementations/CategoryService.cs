using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory addCategory)
        {
            var mappedData = mapper.Map<Category>(addCategory);
            int result = await categoryInterface.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category Added!") :
               new ServiceResponse(false, "Category failed to Add!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category Deleted!") :
                new ServiceResponse(false, "Category failed to Delete!");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await categoryInterface.GetAllAsync();
            if (!rawData.Any()) return [];

            return mapper.Map<IEnumerable<GetCategory>>(rawData);

        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await categoryInterface.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();

            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory updateCategory)
        {
            var mappedData = mapper.Map<Category>(updateCategory);
            int result = await categoryInterface.UpdateAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category Updated!") :
               new ServiceResponse(false, "Category failed to Updated!");
        }
    }
}