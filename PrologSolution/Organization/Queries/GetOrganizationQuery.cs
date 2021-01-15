using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PrologSolution.Data;
using PrologSolution.Data.Entities;

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
            var response = new List<OrganizationViewModel>();
            var phonesList = new List<List<Phone>>();
            var organizationList = await _service.GetOrganizationList();
            foreach (var organization in organizationList)
            {
                int blackList = 0;
                int totalCount = 0;
                var organizationViewModel = new OrganizationViewModel();
                var organizationId = organization.Id;
                organizationViewModel.Id = organizationId;
                organizationViewModel.Name = organization.Name;
                
                organizationViewModel.Users = new List<UsersViewModel>();
                var userList = await _service.GetUsersList(organization.Id);

                foreach (var user in userList)
                {
                    var uvm = new UsersViewModel
                    {
                        Email = user.Email,
                        Id = user.Id
                    };

                    Thread.Sleep(2500);
                    var ph = await _service.GetPhonesList(user.OrganizationId, user.Id);

                    phonesList.Add(ph);
                    int phoneCount = 0;
                    foreach (var p in ph)
                    {
                        phoneCount++;
                        totalCount++;
                        if (p.Blacklist)
                            blackList++;
                    }
                    uvm.PhoneCount = phoneCount;
                    organizationViewModel.BlackListTotal = blackList;
                    organizationViewModel.Users.Add(uvm);
                }
                organizationViewModel.TotalCount = totalCount;
                response.Add(organizationViewModel);
            }
            var viewModel = _mapper.Map<IEnumerable<OrganizationViewModel>>(response);
            return await Task.FromResult(viewModel);
        }

    }
}
