using System;
using PrologSolution.Data.Entities;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            //Comment out line 32-35 and Uncomment this to reveal the underlying Rate limiting error.
            //This error is likely caused by Rate limit set by the API. Need documentation to make batch requests if possible.
            #region Reproduce Error "Response status code does not indicate success: 429 (Too Many Requests)"

            //HttpClient _client = new HttpClient();
            //using (HttpResponseMessage response = await _client.GetAsync(
            //        $"https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/{organizationId}/users/{userId}/phones")
            //    .ConfigureAwait(false))

            //using (HttpContent content = response.Content)
            //{
            //    response.EnsureSuccessStatusCode();
            //    string data = content.ReadAsStringAsync().Result;
            //    return JsonConvert.DeserializeObject<List<Entities.Phone>>(data);
            //}
            #endregion
        }

        public async Task<List<User>> GetUsersList(string organizationId)
        {
            var request = new RestRequest($"organizations/{organizationId}/users", Method.GET);
            var response = _client.Execute<List<User>>(request);
            return await Task.FromResult(response.Data);
        }
    }
}
