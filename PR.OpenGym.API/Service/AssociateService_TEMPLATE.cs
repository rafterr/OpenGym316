using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.API.Service
{
    public class AssociateService_TEMPLATE //: IAssociateService
    {
        IAssociateRepository _associateRepository;
        public AssociateService_TEMPLATE(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _associateRepository.DeleteAsync(id);
            return result;
        }

        public async Task<Associate> GetAsync(int id)
        {
            var result = await _associateRepository.GetAsync(id);
            return result;
        }

        public async Task<IEnumerable<Associate>> GetAllAsync()
        {
            var result = await _associateRepository.GetAllAsync();
            return result;
        }

        public async Task<Associate> PostAsync(Associate associate)
        {
            associate.ModifiedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            associate.CreatedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            var result = await _associateRepository.CreateAsync(associate);
            return result;
        }

        public async Task<bool> PutAsync(Associate associate)
        {
            associate.ModifiedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            var result = await _associateRepository.UpdateAsync(associate);
            return result;
        }
    }
}
