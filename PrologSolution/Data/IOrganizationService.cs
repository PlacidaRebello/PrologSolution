using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrologSolution.Data
{
    public interface IOrganizationService
    {
        Task<List<Phone>> GetPhonesList(int organizationId, int userId);
    }
}
