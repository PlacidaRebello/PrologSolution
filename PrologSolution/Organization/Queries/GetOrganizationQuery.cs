using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrologSolution.Organization.Queries
{
    public class GetOrganizationQuery: IRequest<List<OrganizationViewModel>>
    {

    }
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, List<OrganizationViewModel>>
    {
        private readonly IMapper _mapper;

        public GetOrganizationQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<OrganizationViewModel>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var response = new List<OrganizationViewModel>();
            var viewModel = _mapper.Map<List<OrganizationViewModel>>(response);

            return await Task.FromResult(viewModel);
        }
    }
}
