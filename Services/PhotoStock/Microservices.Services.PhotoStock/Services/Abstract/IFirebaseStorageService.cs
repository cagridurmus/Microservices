using System;
namespace Microservices.Services.PhotoStock.Services.Abstract
{
	public interface IFirebaseStorageService
	{
        Task<Uri> UploadFile(string name, IFormFile file, CancellationToken cancellationToken);

        Task DeleteFile(string fileName, CancellationToken cancellationToken);

        Task<byte[]> GetFile(string fileName, CancellationToken cancellationToken);

    }
}

