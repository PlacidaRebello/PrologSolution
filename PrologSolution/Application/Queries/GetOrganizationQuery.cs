using MediatR;
using PrologSolution.Data;
using PrologSolution.Organization.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrologSolution.Application.Queries
{
    public class GetOrganizationQuery : IRequest<IEnumerable<OrganizationViewModel>>
    {

    }
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, IEnumerable<OrganizationViewModel>>
    {
        private readonly IOrganizationService _service;

        public GetOrganizationQueryHandler(IOrganizationService organizationService)
        {
            _service = organizationService;
        }

        public async Task<IEnumerable<OrganizationViewModel>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var response = new List<OrganizationViewModel>();

            var organizationList = await _service.GetOrganizationList();

            foreach (var organization in organizationList)
            {
                var totalCount = 0;
                var blackList = 0;
                var organizationViewModel = new OrganizationViewModel
                {
                    Id = organization.Id,
                    Name = organization.Name,
                    Users = new List<UsersViewModel>()
                };

                var userList = await _service.GetUsersList(organization.Id);

                foreach (var user in userList)
                {
                    var uvm = new UsersViewModel
                    {
                        Email = user.Email,
                        Id = user.Id
                    };

                    var phoneList = await _service.GetPhonesList(user.OrganizationId, user.Id);

                    var phoneCount = 0;
                    foreach (var phone in phoneList)
                    {
                        phoneCount++;
                        totalCount++;
                        if (phone.Blacklist)
                            blackList++;
                    }

                    uvm.PhoneCount = phoneCount;
                    organizationViewModel.BlackListTotal = blackList;
                    organizationViewModel.Users.Add(uvm);
                }
                organizationViewModel.TotalCount = totalCount;
                response.Add(organizationViewModel);
            }
            return await Task.FromResult(response);
        }

    }
}
