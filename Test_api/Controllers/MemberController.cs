using Microsoft.AspNetCore.Mvc;
using Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_api.Logic.Interfaces;

namespace Test_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : Controller
    {
        private IMemberLogic _MemberLogic;
        public MemberController(IMemberLogic MemberLogic)
        {
            _MemberLogic = MemberLogic;
        }

        [Route("search")]
        public async Task<ActionResult> PostMemberSearch([FromBody] string SearchTerm)
        {
            try
            {
                string ErrorMessage;

                IEnumerable<MemberViewModel> _result = await Task.FromResult(_MemberLogic.GetMembers(SearchTerm, out ErrorMessage));
                if (_result != null)
                    return Ok(_result);
                else
                    return BadRequest(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("add")]
        public async Task<ActionResult> PostMember([FromBody] MemberUpdateModel Member)
        {
            try
            {
                string ErrorMessage;

                MemberViewModel _result = await Task.FromResult(_MemberLogic.AddMember(Member, out ErrorMessage));
                if (_result != null)
                    return Ok(_result);
                else
                    return BadRequest(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Route("edit")]
        public async Task<ActionResult> PutMember([FromBody] MemberUpdateModel Member)
        {
            try
            {
                string ErrorMessage;

                MemberViewModel _result = await Task.FromResult(_MemberLogic.EditMember(Member, out ErrorMessage));
                if (_result != null)
                    return Ok(_result);
                else
                    return BadRequest(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("delete")]
        public async Task<ActionResult> DeleteMember(long Id)
        {
            try
            {
                string ErrorMessage;

                bool _result = await Task.FromResult(_MemberLogic.DeleteMember(Id, out ErrorMessage));
                if (_result)
                    return Ok(_result);
                else
                    return BadRequest(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
