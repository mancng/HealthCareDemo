using Microsoft.AspNetCore.Mvc;
using Models.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_api.Logic.Interfaces;

namespace Test_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralController : Controller
    {
        private IGeneralLogic _GeneralLogic;

        public GeneralController(IGeneralLogic GeneralLogic)
        {
            _GeneralLogic = GeneralLogic;
        }

       [Route("specialties")]
        public async Task<ActionResult> GetSpecialties()
        {
            try
            {
                string ErrorMessage;

                IEnumerable<SpecialtyViewModel> _result = await Task.FromResult(_GeneralLogic.GetAllSpecialties(out ErrorMessage));
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
    }
}
