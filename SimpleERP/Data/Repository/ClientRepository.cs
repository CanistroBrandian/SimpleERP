using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Data.Repository
{
    public class ClientRepository : CommonRepository<Client, string>, IClientRepository
    {
        public ClientRepository(ContextEF context) : base(context)
        {
        }
    }
}
