using AutoMapper;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.API.Service
{
    public class AssociateService : GenericService<Associate>, IAssociateService
    {
        private readonly IAssociateRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public AssociateService(
            IAssociateRepository repository,
            IProductRepository productRepository,
            IMapper mapper) : base(repository)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<AssociateGetDTO> GetAsync(int associateId)
        {
            var associate = await _repository.GetAsync(associateId);
            var associateDTO = _mapper.Map<AssociateGetDTO>(associate);
            return associateDTO;
        }

        public async Task<List<AssociateGetDTO>> GetByNameAsync(string associateFullname) 
        {
            var associates = await _repository.GetByNameAsync(associateFullname);
            var asociatesDto = _mapper.Map<List<AssociateGetDTO>>(associates);
            return asociatesDto;
        }

        public async Task<List<AssociateGetDTO>> GetAllAsync()
        {
            IEnumerable<Associate> associates = await _repository.GetAllAsync();
            var asociates = _mapper.Map<List<AssociateGetDTO>>(associates);
            return asociates;
        }

        public async Task<Associate> PostAsync(AssociatePostDTO data)
        {

            var associate = _mapper.Map<Associate>(data);
            if (associate.AssociateMembership == null)
                associate.AssociateMembership = new AssociateMembership();

            Membership membership = await _productRepository.GetMembeship(data.AssociateMembership.MembershipId);
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            var from = data.AssociateMembership.From ?? now;
            associate.CreatedOn = now;
            associate.ModifiedOn = now;
            associate.AssociateMembership.CreatedOn = now;
            associate.AssociateMembership.ModifiedOn = now;
            associate.AssociateMembership.From = from;
            associate.AssociateMembership.To = UpdateDatePeriod(from, membership.Period);
            associate.Status = Status.Active;
            associate = await _repository.CreateAsync(associate);

            return associate;
        }

        public async Task<Associate> PutAsync(int associateId, AssociatePostDTO data)
        {
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();

            var associate = await _repository.GetAsync(associateId);
            if (associate.Age != data.Age)
                associate.Age = data.Age;

            if (associate.FirstName != data.FirstName)
                associate.FirstName = data.FirstName;

            if (associate.LastName != data.LastName)
                associate.LastName = data.LastName;

            if (associate.BranchId != data.BranchId)
                associate.BranchId = data.BranchId;

            if (associate.Email != data.Email)
                associate.Email = data.Email;

            if (associate.Gender != data.Gender)
                associate.Gender = data.Gender;

            if (associate.Facebook != data.Facebook)
                associate.Facebook = data.Facebook;

            if (associate.Phone != data.Phone)
                associate.Phone = data.Phone;

            if (data.AssociateMembership != null && 
                (associate.AssociateMembership?.MembershipId != data.AssociateMembership?.MembershipId  || data.AssociateMembership.From != associate.AssociateMembership.From))
            {

                Membership membership = await _productRepository.GetMembeship(data.AssociateMembership.MembershipId);
                if (membership != null)
                {
                    associate.AssociateMembership.ModifiedOn = now;
                    if (associate.AssociateMembership.From != data.AssociateMembership.From) 
                    {
                        associate.AssociateMembership.From = data.AssociateMembership.From;
                        associate.AssociateMembership.To = UpdateDatePeriod(data.AssociateMembership.From.Value, membership.Period);
                    }
                    if (associate.AssociateMembership.MembershipId!= data.AssociateMembership.MembershipId)
                        associate.AssociateMembership.MembershipId = data.AssociateMembership.MembershipId;
                }
            }
            if (associate.Status != data.Status)
                associate.Status = data.Status;

            await _repository.UpdateAsync(associate);
            return associate;
        }


        public async Task<IEnumerable<CheckIn>> GetAllCheckInsByAssociate(int associateId)
        {
            var checkins = await _repository.GetCheckIns(associateId);
            return checkins;
        }

        public async Task<IEnumerable<CheckIn>> GetAllCheckInsByDate(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                dateTime = DateTime.Now.ConvertDateToMexicoCentralLocalZone();

            var checkins = await _repository.GetCheckIns(dateTime);
            return checkins;
        }

        public async Task PostCheckIn(int associateId)
        {
            var checkIn = new CheckIn()
            {
                AssociateId = associateId,
                CreatedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone(),
                ModifiedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone()
            };
            await _repository.AddCheckIn(checkIn);
        }

        public async Task<AssociateMembership> GetAssociateMembershipByAssociateIdAsync(int associateId)
        {
            return await _repository.GetAssociateMembershipByAssociateIdAsync(associateId);
        }

        public async Task<AssociateMembership> CreateAssociateMembershipAsync(AssociateMembership associateMembership)
        {
            var membership = await _productRepository.GetMembeship(associateMembership.MembershipId.Value);
            var associate = await _productRepository.GetAsync(associateMembership.Associate.Id);
            if (membership == null || associate == null)
                throw new ArgumentException("Membership not found");

            UpdateAssociateMembershipActiveDates(membership, associateMembership, null);
            var res = await _repository.AddAssociateMembershipAsync(associateMembership);
            return res;
        }

        public async Task<bool> UpdateAssociateMembershipAsync(AssociateMembership associateMembership)
        {
            int membershipId = associateMembership.MembershipId.Value;
            var membership = await _productRepository.GetMembeship(membershipId);
            if (membership == null)
                throw new ArgumentException("Product not found");

            UpdateAssociateMembershipActiveDates(membership, associateMembership, associateMembership.From, false);
            var res = await _repository.UpdateAssociateMembershipAsync(membershipId, associateMembership);
            return res;
        }

        private void UpdateAssociateMembershipActiveDates(Membership membership, AssociateMembership associateMembership, DateTime? startDate, bool isCreate = true)
        {
            DateTime now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            DateTime from = startDate != null ? startDate.Value : now;
            if (isCreate)
                associateMembership.CreatedOn = now;
            associateMembership.From = from;
            associateMembership.To = UpdateDatePeriod(from, membership.Period);

            associateMembership.ModifiedOn = now;
            associateMembership.MembershipStatus = MembershipStatus.Active;

        }

        private DateTime UpdateDatePeriod(DateTime from,int period) {
            DateTime to;
            if (period == 30)
                to = from.AddMonths(1);
            else if (period == 365)
                to = from.AddYears(1);
            else
               to = from.AddDays(period);
            return to;
        }

    }
}
