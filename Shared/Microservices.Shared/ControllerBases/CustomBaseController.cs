using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Shared.ControllerBases
{
    [Route("api/[controller]")]
	[ApiController]
    public class CustomBaseController: ControllerBase
	{
		[NonAction]
		public IActionResult CreateActionResult<T>(ResponseDto<T> response)
		{
			return new ObjectResult(response)
			{
				StatusCode = response.StatusCode
			};
		}

    }
}

