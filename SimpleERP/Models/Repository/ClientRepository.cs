using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Models.Repository
{
    public class ClientRepository : CommonRepository<Client, string>, IClientRepository
    {
        public ClientRepository(ContextEF context) : base(context)
        {
        }
    }
}
