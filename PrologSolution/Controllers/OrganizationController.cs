using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrologSolution.Organization.Queries;

namespace PrologSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<OrganizationViewModel> Get()
        {
            return new List<OrganizationViewModel>();
        }
    }
}
