using System.Collections.Generic;


namespace PrologSolution.Organization.Queries
{
    public class OrganizationViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int BlackListTotal { get; set; }
        public int TotalCount { get; set; }
        public IList<UsersViewModel> Users { get; set; }
    }
}
