using Microservices.Services.Catalog.Models;
using Microservices.Shared.Dtos;

namespace Microservices.Services.Catalog.Services
{
    public interface ICategoryService
	{
        Task<ResponseDto<List<Category>>> GetAllAsync();

        Task<ResponseDto<Category>> CreateAsync(Category category);

        Task<ResponseDto<Category>> GetByIdAsync(string id);

    }
}

