using Models.Member;
using System.Collections.Generic;

namespace Test_api.Logic.Interfaces
{
    public interface IMemberLogic
    {
        IEnumerable<MemberViewModel> GetMembers(string SearchTerm, out string ErrorMessage);
        MemberViewModel AddMember(MemberUpdateModel Member, out string ErrorMessage);
        MemberViewModel EditMember(MemberUpdateModel Member, out string ErrorMessage);
        bool DeleteMember(long Id, out string ErrorMessage);
    }
}
