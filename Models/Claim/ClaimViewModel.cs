using Models.General;
using System;

namespace Models.Claim
{
    public class ClaimViewModel : BaseViewModel
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long ProviderId { get; set; }
        public DateTime DateOfService { get; set; }
    }
}
