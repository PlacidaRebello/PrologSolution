using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrologSolution.Organization.Queries;

namespace PrologSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<OrganizationViewModel>> Get()
        {
            return await Mediator.Send(new GetOrganizationQuery());
        }
    }
}
