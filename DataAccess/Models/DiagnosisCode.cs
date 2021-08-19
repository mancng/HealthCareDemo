using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class DiagnosisCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
