using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ProviderSpeciality
    {
        public long ProviderId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual Speciality Specialty { get; set; }
    }
}
