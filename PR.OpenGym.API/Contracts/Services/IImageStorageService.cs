using Microsoft.AspNetCore.Http.Metadata;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IImageStorageService
    {
        Task<string> SaveImageDocumentManagement(int associateId,IFormFile imgFile);
    }
}
