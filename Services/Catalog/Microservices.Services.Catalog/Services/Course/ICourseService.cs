using Microservices.Shared.Dtos;

namespace Microservices.Services.Catalog.Services.Course
{
    public interface ICourseService
	{
        Task<ResponseDto<List<Models.Course>>> GetAllAsync();

        Task<ResponseDto<Models.Course>> GetByIdAsync(string id);

        Task<ResponseDto<List<Models.Course>>> GetAllByUserIdAsync(string userId);

        Task<ResponseDto<Models.Course>> CreateAsync(Models.Course course);

        Task<ResponseDto<NoContentDto>> UpdateAsync(Models.Course course);

        Task<ResponseDto<NoContentDto>> DeleteAsync(string id);

    }
}

