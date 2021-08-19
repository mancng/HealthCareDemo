using Models.General;
using System.Collections.Generic;

namespace Test_api.Logic.Interfaces
{
    public interface IGeneralLogic
    {
        IEnumerable<SpecialtyViewModel> GetAllSpecialties(out string ErrorMessage);
    }
}
