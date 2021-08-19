using DataAccess.Models;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Test_api.Logic.Interfaces;
using Test_api.Repository.Implementations;


namespace Test_api.Logic.Implementations
{
    public class GeneralLogic : IGeneralLogic
    {
        internal readonly KPDemoContext _context;
        private UnitOfWorks _unitOfWorks;
        public GeneralLogic(KPDemoContext context)
        {
            _context = context;
            _unitOfWorks = new UnitOfWorks(_context);
        }

        public IEnumerable<SpecialtyViewModel> GetAllSpecialties(out string ErrorMessage)
        {
            try
            {
                IQueryable<Speciality> _specialties = _unitOfWorks.Speciality.Get();

                IEnumerable<SpecialtyViewModel> _result = _specialties
                    .Select(p => new SpecialtyViewModel
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Description = p.Description,
                        CreatedDate = p.CreatedDate,
                        ModifiedDate = p.ModifiedDate
                    })
                    .ToList();

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
