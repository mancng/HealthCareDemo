using Models.General;
using System.Collections.Generic;

namespace Models.Provider
{
    public class ProviderViewModel : BaseViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public IEnumerable<ProviderSpecialtyViewModel> ProviderSpecialties { get; set; }
    }
}
