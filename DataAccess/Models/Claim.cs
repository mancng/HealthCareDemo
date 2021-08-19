using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Claim
    {
        public long MemberId { get; set; }
        public long ProviderId { get; set; }
        public DateTime DateOfService { get; set; }
        public int DiagnosisCodeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual DiagnosisCode DiagnosisCode { get; set; }
        public virtual Member Member { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
