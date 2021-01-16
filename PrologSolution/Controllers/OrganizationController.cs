using Microsoft.AspNetCore.Mvc;
using PrologSolution.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrologSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController : ApiController
    {
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<IEnumerable<OrganizationViewModel>> Get()
        {
            return await Mediator.Send(new GetOrganizationQuery());
        }
    }
}
