using Microsoft.EntityFrameworkCore;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.API.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly PROpenGymWebContext _dbContext;

        public PaymentRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Payment>> GetTodayPayments()
        {
            var today = DateTime.Today.ConvertDateToMexicoCentralLocalZone().Date;
            return _dbContext.Payments.Where(p => p.CreatedOn.Date == today);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId)
        {
            var today = DateTime.Today.ConvertDateToMexicoCentralLocalZone().Date;
            return _dbContext.Payments.Include(p=>p.Product).Where(p => p.AssociateId == associateId);
        }

    }
}
