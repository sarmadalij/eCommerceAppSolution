using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory addCategory);
        Task<ServiceResponse> UpdateAsync(UpdateCategory updateCategory);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }


}
