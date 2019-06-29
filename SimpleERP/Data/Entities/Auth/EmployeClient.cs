namespace SimpleERP.Data.Entities.Auth
{
    public class EmployeClient
    {
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
    }
}
