namespace PrologSolution.Data.Entities
{
    public class Phone
    {
        public string Id { get; set; }
        public string  UserId { get; set; }
        public string CreatedAt { get; set; }
        public int IMEI { get; set; }
        public bool Blacklist { get; set; }
    }
}
