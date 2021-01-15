using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrologSolution.Data;

namespace PrologSolution.Organization.Queries
{
    public class GetOrganizationQuery: IRequest<IEnumerable<OrganizationViewModel>>
    {

    }
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, IEnumerable<OrganizationViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationService _service;

        public GetOrganizationQueryHandler(IMapper mapper, IOrganizationService organizationService)
        {
            _mapper = mapper;
            _service = organizationService;
        }

        public async Task<IEnumerable<OrganizationViewModel>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var data = await _service.GetPhonesList(organizationId:1, userId:1);
            var response = new List<OrganizationViewModel>();
            var viewModel = _mapper.Map<IEnumerable<OrganizationViewModel>>(response);

            return await Task.FromResult(viewModel);
        }
    }
}
