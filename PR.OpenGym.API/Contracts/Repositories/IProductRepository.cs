using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<Membership>> GetMembeships();
        Task<Membership> GetMembeship(int id);
    }
}
