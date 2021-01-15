using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrologSolution.Data;
using PrologSolution.Data.Entities;
using System.Threading;

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
            var phonesList=new List<Phone>();
            var usersList= new List<User>();
            var organizationList = await _service.GetOrganizationList();
            foreach (var organization in organizationList)
            {
                var a = new OrganizationViewModel();
                var organizationId = organization.Id;
                a.Id = organizationId;
                a.Name = organization.Name;
                
                usersList = await _service.GetUsersList(organizationId: organizationId);

                if(usersList != null)
                {
                    //Parallel.ForEach(usersList, i => DoSomething(i.Id).Wait());
                    foreach (var user in usersList)
                    {
                        var userId = user.Id;
                        var datausers = new UsersViewModel();
                        datausers.Id = user.Id;
                        datausers.Email = user.Email;
                        a.Users = new List<UsersViewModel>();
                        a.Users.Add(datausers);
                        phonesList = await _service.GetPhonesList(organizationId: organizationId, userId: userId);
                    }
                }
                response.Add(a);
            }

            var viewModel = _mapper.Map<IEnumerable<OrganizationViewModel>>(response);

            return await Task.FromResult(viewModel);
        }
    }
}
