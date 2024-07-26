using Microservices.Shared.ControllerBases;
using Microservices.Shared.Dtos;
using Microservices.UserAPI.Dtos;
using Microservices.UserAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityServer.IdentityServerConstants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.UserAPI.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    public class UserController : CustomBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]

        public async Task<IActionResult> SignUp(SignUpDto signUp)
        {
            var user = new ApplicationUser
            {
                Email = signUp.Email,
                UserName = signUp.UserName,

            };

            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (result.Succeeded)
            {
                return CreateActionResult(ResponseDto<bool>.Success(result.Succeeded, 201));
            }
            else
            {
                return CreateActionResult(ResponseDto<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }

            
        }
    }
}

