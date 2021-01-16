using PrologSolution.Data.Entities;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrologSolution.Data
{
    public class OrganizationService : IOrganizationService
    {
        private readonly RestClient _client = new RestClient("https://5f0ddbee704cdf0016eaea16.mockapi.io/");

        public async Task<List<Entities.Organization>> GetOrganizationList()
        {
            var request = new RestRequest($"organizations", Method.GET);
            var response = _client.Execute<List<Entities.Organization>>(request);
            return await Task.FromResult(response.Data);
        }

        public async Task<List<Phone>> GetPhonesList(string organizationId, string userId)
        {
            Thread.Sleep(2000);
            var request = new RestRequest($"organizations/{organizationId}/users/{userId}/phones", Method.GET);
            var response = _client.Execute<List<Phone>>(request);
            return await Task.FromResult(response.Data);
        }

        public async Task<List<User>> GetUsersList(string organizationId)
        {
            var request = new RestRequest($"organizations/{organizationId}/users", Method.GET);
            var response = _client.Execute<List<User>>(request);
            return await Task.FromResult(response.Data);
        }
    }
}
