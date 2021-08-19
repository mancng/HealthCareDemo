using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Test_api.Utilities;
using Utilities;

namespace Test_api.Repository.Implementations
{
    public class UnitOfWorks
    {
        private readonly DbContext _context;
        private IGenericRepository<Claim> claimRepository;
        private IGenericRepository<DiagnosisCode> diagnosisCodeRepository;
        private IGenericRepository<Member> memberRepository;
        private IGenericRepository<Provider> providerRepository;
        private IGenericRepository<ProviderSpeciality> providerSpecialityRepository;
        private IGenericRepository<Speciality> specialityRepository;

        public UnitOfWorks(DbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Claim> Claim
        {
            get
            {
                if (claimRepository == null)
                    claimRepository = new GenericRepository<Claim>(_context);

                return claimRepository;
            }
        }

        public IGenericRepository<DiagnosisCode> DiagnosisCode
        {
            get
            {
                if (diagnosisCodeRepository == null)
                    diagnosisCodeRepository = new GenericRepository<DiagnosisCode>(_context);

                return diagnosisCodeRepository;
            }
        }

        public IGenericRepository<Member> Member
        {
            get
            {
                if (memberRepository == null)
                    memberRepository = new GenericRepository<Member>(_context);

                return memberRepository;
            }
        }

        public IGenericRepository<Provider> Provider
        {
            get
            {
                if (providerRepository == null)
                    providerRepository = new GenericRepository<Provider>(_context);

                return providerRepository;
            }
        }

        public IGenericRepository<ProviderSpeciality> ProviderSpeciality
        {
            get
            {
                if (providerSpecialityRepository == null)
                    providerSpecialityRepository = new GenericRepository<ProviderSpeciality>(_context);

                return providerSpecialityRepository;
            }
        }

        public IGenericRepository<Speciality> Speciality
        {
            get
            {
                if (specialityRepository == null)
                    specialityRepository = new GenericRepository<Speciality>(_context);

                return specialityRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
