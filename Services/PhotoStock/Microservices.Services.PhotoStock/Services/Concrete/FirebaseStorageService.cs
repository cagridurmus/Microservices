using System.Globalization;
using System.IO;
using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microservices.Services.PhotoStock.Models;
using Microservices.Services.PhotoStock.Services.Abstract;
using Microsoft.Extensions.Options;

namespace Microservices.Services.PhotoStock.Services.Concrete
{
    public class FirebaseStorageService: IFirebaseStorageService
	{
        private readonly StorageClient _storageClient;
        private readonly IOptions<GoogleOptions> _options;
        private readonly GoogleCredential _googleCredential;

        public FirebaseStorageService(IOptions<GoogleOptions> options)
        {
            _options = options;
            _googleCredential = GoogleCredential.FromFile(_options.Value.GoogleCredentialFilePath);
            _storageClient = StorageClient.Create(_googleCredential);
            
        }

        public async Task<Uri> UploadFile(string fileName, IFormFile file, CancellationToken cancellationToken)
        {
            var randomGuid = Guid.NewGuid();
            
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);
            var blob = await _storageClient.UploadObjectAsync(_options.Value.GoogleCloudStorageBucketName,
                $"{fileName}-{randomGuid}", file.ContentType, stream, cancellationToken: cancellationToken);
            var photoUri = new Uri(blob.MediaLink);
            return photoUri;
        }

        public async Task DeleteFile(string fileName, CancellationToken cancellationToken)
        {
           await _storageClient.DeleteObjectAsync(_options.Value.GoogleCloudStorageBucketName,
                fileName , cancellationToken: cancellationToken);
        }
        
        public async Task<byte[]> GetFile(string fileName, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await _storageClient.DownloadObjectAsync(_options.Value.GoogleCloudStorageBucketName, fileName, memoryStream, cancellationToken: cancellationToken);
            
            memoryStream.Position = 0; 
            return memoryStream.ToArray();

        }
    }
}

