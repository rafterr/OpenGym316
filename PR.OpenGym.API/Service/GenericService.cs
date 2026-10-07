using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.API.Repository;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.API.Service
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        IGenericRepository<TEntity> _repository;
        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return result;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var result = await _repository.GetAsync(id);
            return result;
        }

        public async Task<TEntity> PostAsync(TEntity entity)
        {
            entity.GetType().GetProperty("ModifiedOn")?.SetValue(entity, DateTime.Now.ConvertDateToMexicoCentralLocalZone());
            entity.GetType().GetProperty("CreatedOn")?.SetValue(entity, DateTime.Now.ConvertDateToMexicoCentralLocalZone());
            var result = await _repository.CreateAsync(entity);
            return result;
        }

        public async Task<bool> PutAsync(TEntity entity)
        {
            entity.GetType().GetProperty("ModifiedOn")?.SetValue(entity, DateTime.Now.ConvertDateToMexicoCentralLocalZone());
            var result = await _repository.UpdateAsync(entity);
            return result;
        }
    }
}
