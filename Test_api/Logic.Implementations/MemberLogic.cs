using AutoMapper;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Test_api.Logic.Interfaces;
using Test_api.Repository.Implementations;

namespace Test_api.Logic.Implementations
{
    public class MemberLogic : IMemberLogic
    {
        internal readonly KPDemoContext _context;
        private UnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public MemberLogic(KPDemoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWorks = new UnitOfWorks(_context);
        }

        public IEnumerable<MemberViewModel> GetMembers(string SearchTerm, out string ErrorMessage)
        {
            try
            {
                IQueryable<Member> _members;

                string _term = !String.IsNullOrEmpty(SearchTerm) ? SearchTerm.Trim() : null;

                if (String.IsNullOrEmpty(_term))
                {
                    _members = _unitOfWorks.Member.Get();
                }
                else
                {
                    _members = _unitOfWorks.Member.Get(filter: r => r.FirstName == _term || r.LastName == _term);
                }

                IEnumerable<MemberViewModel> _result = _mapper.Map<IEnumerable<MemberViewModel>>(_members).Select(r => r).ToList();

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public MemberViewModel AddMember(MemberUpdateModel Member, out string ErrorMessage)
        {
            try
            {
                if (Member == null || Member.Id != 0)
                {
                    ErrorMessage = "Invalid Member.";
                    return null;
                };

                Member newMember = _unitOfWorks.Member.Insert(new Member
                {
                    FirstName = Member.FirstName.Trim(),
                    LastName = Member.LastName.Trim(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });

                _unitOfWorks.Save();

                MemberViewModel _result = _mapper.Map<MemberViewModel>(_unitOfWorks.Member.Get(filter: r => r.Id == newMember.Id));

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public MemberViewModel EditMember(MemberUpdateModel Member, out string ErrorMessage)
        {
            try
            {
                if (Member == null || Member.Id < 1)
                {
                    ErrorMessage = "Missing Member.";
                    return null;
                };

                Member _currentMember = _unitOfWorks.Member.GetById(Member.Id);

                _currentMember.FirstName = Member.FirstName.Trim();
                _currentMember.LastName = Member.LastName.Trim();
                _currentMember.ModifiedDate = DateTime.Now;

                _unitOfWorks.Save();

                MemberViewModel _result = _mapper.Map<MemberViewModel>(_unitOfWorks.Member.Get(filter: r => r.Id == _currentMember.Id));

                ErrorMessage = "";
                return _result;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public bool DeleteMember(long Id, out string ErrorMessage)
        {
            try
            {
                Member _currentMember = _unitOfWorks.Member.GetById(Id);
                if (_currentMember == null)
                {
                    ErrorMessage = "No Member Found";
                    return false;
                };

                _unitOfWorks.Member.Delete(_currentMember);
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
