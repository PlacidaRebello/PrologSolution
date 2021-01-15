using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace PrologSolution.Data
{
    public class OrganizationService : IOrganizationService
    {
        private readonly RestClient _client = new RestClient("https://5f0ddbee704cdf0016eaea16.mockapi.io/");
        public async Task<List<Phone>> GetPhonesList(int organizationId, int userId)
        {
            var request = new RestRequest($"organizations/{organizationId}/users/{userId}/phones", Method.GET);
            var response = _client.Execute<List<Phone>>(request);
            return await Task.FromResult(response.Data);
        }
    }
}
