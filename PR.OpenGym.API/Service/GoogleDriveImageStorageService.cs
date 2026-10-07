using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Service
{
    public class GoogleDriveImageStorageService : IImageStorageService
    {
        private readonly string _credentials;
        private readonly string _sharedDirectoryId;
        private readonly DriveService _service;

        public GoogleDriveImageStorageService(string credentials,string sharedDirectoryId)
        {
            _credentials = credentials;
            _sharedDirectoryId = sharedDirectoryId;
            var credencials = GoogleCredential.FromJson(_credentials).CreateScoped(DriveService.ScopeConstants.Drive);
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credencials
            });
        }
        public async Task<string> SaveImageDocumentManagement(int associateId, IFormFile imgFile)
        {
            var id = await UploadFile(associateId, imgFile);
            return $@"https://drive.google.com/uc?export=view&id={id}";

        }

        private async Task<string> UploadFile(int associateId,IFormFile imgFile) {         


            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = $"{associateId}.jpg",
                Parents = new List<string>() { _sharedDirectoryId },

            };

            string uploadedFileId;
            var request = _service.Files.Create(fileMetadata, imgFile.OpenReadStream(), "image/jpg");
            request.Fields = "*";
            var result = await request.UploadAsync(CancellationToken.None);
            if (result.Status == Google.Apis.Upload.UploadStatus.Failed)
            {
                throw new Exception("Error al subir archivo :(  " + result.Exception.Message);
            }
            uploadedFileId = request.ResponseBody?.Id;
            return uploadedFileId;
        }       
    }
}
