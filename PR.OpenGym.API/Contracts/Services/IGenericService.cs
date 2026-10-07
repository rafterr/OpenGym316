using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> PostAsync(T entity);
        Task<bool> PutAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
