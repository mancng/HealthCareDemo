using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Provider
    {
        public Provider()
        {
            ProviderSpecialties = new HashSet<ProviderSpeciality>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProviderSpeciality> ProviderSpecialties { get; set; }
    }
}
