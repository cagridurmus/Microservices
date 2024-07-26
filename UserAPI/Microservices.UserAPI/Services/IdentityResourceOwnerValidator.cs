using System;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microservices.UserAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Microservices.UserAPI.Services
{
	public class IdentityResourceOwnerValidator: IResourceOwnerPasswordValidator
	{
		private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}

