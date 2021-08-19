using Models.General;
using Models.Provider;
using System.Collections.Generic;

namespace Test_api.Logic.Interfaces
{
    public interface IProviderLogic
    {
        IEnumerable<ProviderViewModel> GetProviders(string SearchTerm, out string ErrorMessage);
        ProviderViewModel AddProvider(ProviderUpdateModel Provider, out string ErrorMessage);
        ProviderViewModel EditProvider(ProviderUpdateModel Provider, out string ErrorMessage);
        bool DeleteProvider(long Id, out string ErrorMessage);
        
    }
}
