using AutoMapper;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models.General;
using Models.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Test_api.Logic.Interfaces;
using Test_api.Repository.Implementations;

namespace Test_api.Logic.Implementations
{
    public class ProviderLogic : IProviderLogic
    {
        internal readonly KPDemoContext _context;
        private UnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public ProviderLogic(KPDemoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWorks = new UnitOfWorks(_context);
        }

        public IEnumerable<ProviderViewModel> GetProviders(string SearchTerm, out string ErrorMessage)
        {
            try
            {
                IQueryable<Provider> _providers;

                string _term = !String.IsNullOrEmpty(SearchTerm) ? SearchTerm.Trim() : null;

                if (String.IsNullOrEmpty(_term))
                {
                    _providers = _unitOfWorks.Provider.Get().Include(r => r.ProviderSpecialties).ThenInclude(g => g.Specialty);
                }
                else
                {
                    _providers = _unitOfWorks.Provider.Get(filter: r => r.FirstName == _term || r.LastName == _term).Include(r => r.ProviderSpecialties).ThenInclude(g => g.Specialty);
                }

                IEnumerable<ProviderViewModel> _result = _mapper.Map<IEnumerable<ProviderViewModel>>(_providers).Select(r => r).ToList();

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public ProviderViewModel AddProvider(ProviderUpdateModel Provider, out string ErrorMessage)
        {
            try
            {
                if (Provider == null || Provider.Id != 0)
                {
                    ErrorMessage = "Invalid Provider.";
                    return null;
                }

                Provider newProvider = _unitOfWorks.Provider.Insert(new Provider
                {
                    FirstName = Provider.FirstName.Trim(),
                    LastName = Provider.LastName.Trim(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });

                if (Provider.ProviderSpecialties.Count() > 0)
                {
                    //Add Provider Specialties
                    IEnumerable<ProviderSpecialtyUpdateModel> _specialties = Provider.ProviderSpecialties;

                    foreach (ProviderSpecialtyUpdateModel specialty in _specialties)
                    {
                        if ((bool)specialty.NewRecord)
                        {

                            Speciality _specialty = _unitOfWorks.Speciality.GetById(specialty.Id);

                            _unitOfWorks.ProviderSpeciality.Insert(new ProviderSpeciality
                            {
                                Provider = newProvider,
                                Specialty = _specialty,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            });
                        }
                    }
                }
                _unitOfWorks.Save();

                Provider _pro = _unitOfWorks.Provider.Get(filter: r => r.Id == newProvider.Id).Include(r => r.ProviderSpecialties).ThenInclude(g => g.Specialty).FirstOrDefault();
                ProviderViewModel _result = _mapper.Map<ProviderViewModel>(_pro);

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public ProviderViewModel EditProvider(ProviderUpdateModel Provider, out string ErrorMessage)
        {
            try
            {
                if (Provider == null || Provider.Id < 1)
                {
                    ErrorMessage = "Missing Provider.";
                    return null;
                }

                Provider _currentProvider = _unitOfWorks.Provider.GetById(Provider.Id);

                _currentProvider.FirstName = Provider.FirstName.Trim();
                _currentProvider.LastName = Provider.LastName.Trim();
                _currentProvider.ModifiedDate = DateTime.Now;

                //Update Provider Specialties
                IEnumerable<ProviderSpecialtyUpdateModel> _specialties = Provider.ProviderSpecialties;

                foreach (ProviderSpecialtyUpdateModel specialty in _specialties)
                {
                    if ((bool)specialty.NewRecord)
                    {
                        Provider _provider = _unitOfWorks.Provider.GetById(Provider.Id);
                        Speciality _specialty = _unitOfWorks.Speciality.GetById(specialty.Id);

                        if (_provider == null || _specialty == null)
                        {
                            ErrorMessage = "Invalid Provider or Specialties.";
                            return null;
                        }

                        _unitOfWorks.ProviderSpeciality.Insert(new ProviderSpeciality
                        {
                            Provider = _provider,
                            Specialty = _specialty,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        });
                    }
                    else if ((bool)specialty.Delete)
                    {
                        ProviderSpeciality _existingRecord = _unitOfWorks.ProviderSpeciality.Get(filter: r =>
                        r.ProviderId == Provider.Id && r.SpecialtyId == specialty.Id).First();
                        _unitOfWorks.ProviderSpeciality.Delete(_existingRecord);
                    }
                }

                _unitOfWorks.Save();

                Provider _pro = _unitOfWorks.Provider.Get(filter: r => r.Id == Provider.Id).Include(r => r.ProviderSpecialties).ThenInclude(g => g.Specialty).FirstOrDefault();
                ProviderViewModel _result = _mapper.Map<ProviderViewModel>(_pro);

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public bool DeleteProvider(long Id, out string ErrorMessage)
        {
            try
            {
                Provider _currentProvider = _unitOfWorks.Provider.GetById(Id);
                if (_currentProvider == null)
                {
                    ErrorMessage = "No Provider Found";
                    return false;
                }

                //Delete Provider Specalities
                List<ProviderSpeciality> providerSpecialities = _unitOfWorks.ProviderSpeciality.Get(filter: r => r.ProviderId == Id).ToList();
                if (providerSpecialities.Count() > 0)
                {
                    foreach (ProviderSpeciality speciality in providerSpecialities)
                    {
                        _unitOfWorks.ProviderSpeciality.Delete(speciality);
                    }
                }

                _unitOfWorks.Provider.Delete(_currentProvider);
                _unitOfWorks.Save();
                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

    }

}
