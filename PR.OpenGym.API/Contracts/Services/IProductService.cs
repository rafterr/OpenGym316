using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(bool isMembership = false);
        Task<Membership> GetMembershipByIdAsync(int membershipId);
    }
}
