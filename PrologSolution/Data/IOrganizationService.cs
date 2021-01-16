using PrologSolution.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrologSolution.Data
{
    public interface IOrganizationService
    {
        Task<List<Phone>> GetPhonesList(string organizationId, string userId);
        Task<List<User>> GetUsersList(string organizationId);
        Task<List<Entities.Organization>> GetOrganizationList();
    }
}
