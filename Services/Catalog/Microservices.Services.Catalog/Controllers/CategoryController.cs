using System;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Services;
using Microservices.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Catalog.Controllers
{
	public class CategoryController: CustomBaseController
	{
		private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            var response = await _categoryService.CreateAsync(category);
            return CreateActionResult(response);
        }
    }
}

