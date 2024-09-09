using Microservices.Services.PhotoStock.Dtos;
using Microservices.Services.PhotoStock.Services.Abstract;
using Microservices.Shared.ControllerBases;
using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.Services.PhotoStock.Controllers
{
    public class PhotosController : CustomBaseController
    {
        IFirebaseStorageService _firebaseStorageService;

        public PhotosController(IFirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursePhoto(string photoName, CancellationToken cancellationToken)
        {
            var byteArray = await _firebaseStorageService.GetFile(photoName, cancellationToken);

            return CreateActionResult(ResponseDto<byte[]>.Success(byteArray, 200));
        }

        [HttpPost]
        public async Task<IActionResult> UploadCoursePhoto(IFormFile uploadFile, CancellationToken cancellationToken)
        {
            var uri = await _firebaseStorageService.UploadFile("1234", uploadFile, cancellationToken);
            
            return CreateActionResult(ResponseDto<PhotoDto?>.Success(new PhotoDto { uri = uri }, 200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCoursePhoto(string photoName, CancellationToken cancellationToken)
        {
            await _firebaseStorageService.DeleteFile(photoName, cancellationToken);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(200));
        }
    }
}

