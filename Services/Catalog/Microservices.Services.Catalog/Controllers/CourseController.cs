using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Services.Catalog.Services.Course;
using Microservices.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Services.Catalog.Controllers
{
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _courseService.GetAllAsync();

            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);

            return CreateActionResult(response);
        }

        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);

            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Models.Course course)
        {
            var response = await _courseService.CreateAsync(course);

            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Models.Course course)
        {
            var response = await _courseService.UpdateAsync(course);

            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _courseService.DeleteAsync(id);

            return CreateActionResult(response);
        }
    }
}

