using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(bool isMembership = false)
        {
            if (isMembership)
                return await _repository.GetMembeships();
            else
                return await _repository.GetAllAsync();
        }

        public async Task<Membership> GetMembershipByIdAsync(int membershipId)
        {
            return await _repository.GetMembeship(membershipId);
        }
    }
}
