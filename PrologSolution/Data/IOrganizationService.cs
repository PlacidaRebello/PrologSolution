using System.Collections.Generic;
using System.Threading.Tasks;
using PrologSolution.Data.Entities;

namespace PrologSolution.Data
{
    public interface IOrganizationService
    {
        Task<List<Phone>> GetPhonesList(int organizationId, int userId);
    }
}
