using SimpleERP.Data.Entities.OrderEntity;

namespace SimpleERP.Data.Entities.Auth
{
    public class EmployeOrder
    {
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
