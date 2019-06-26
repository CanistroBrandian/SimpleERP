using SimpleERP.Data.Entities.OrderEntity;

namespace SimpleERP.Data.Entities.Auth
{
    public class ClientOrder 
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }

    }
}
