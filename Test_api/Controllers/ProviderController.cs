using Microsoft.AspNetCore.Mvc;
using Models.General;
using Models.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_api.Logic.Interfaces;

namespace Test_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : Controller
    {
        private IProviderLogic _ProviderLogic;
        public ProviderController(IProviderLogic ProviderLogic)
        {
            _ProviderLogic = ProviderLogic;
        }

        [Route("search")]
        public async Task<ActionResult> PostProviderSearch([FromBody] string SearchTerm)
        {
            try
            {
                string ErrorMessage;

                //var getData = Task.Run(() => _ProviderLogic.GetProviders(SearchTerm, out ErrorMessage));

                IEnumerable<ProviderViewModel> _result = await Task.FromResult(_ProviderLogic.GetProviders(SearchTerm, out ErrorMessage));
                if (_result != null)
                    return Ok( _result );
                else
                    return BadRequest(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("add")]
        public async Task<ActionResult> PostProvider([FromBody] ProviderUpdateModel Provider)
        {
            try
            {
                string ErrorMessage;

                ProviderViewModel _result = await Task.FromResult(_ProviderLogic.AddProvider(Provider, out ErrorMessage));
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
        public async Task<ActionResult> PutProvider([FromBody] ProviderUpdateModel Provider)
        {
            try
            {
                string ErrorMessage;

                ProviderViewModel _result = await Task.FromResult(_ProviderLogic.EditProvider(Provider, out ErrorMessage));
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
        public async Task<ActionResult> DeleteProvider(long Id)
        {
            try
            {
                string ErrorMessage;

                bool _result = await Task.FromResult(_ProviderLogic.DeleteProvider(Id, out ErrorMessage));
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
