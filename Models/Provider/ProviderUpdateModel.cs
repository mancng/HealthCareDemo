using System.Collections.Generic;

namespace Models.Provider
{
    public class ProviderUpdateModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ProviderSpecialtyUpdateModel> ProviderSpecialties { get; set; }
    }
}
