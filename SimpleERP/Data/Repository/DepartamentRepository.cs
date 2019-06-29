using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;

namespace SimpleERP.Data.Repository
{
    public class DepartamentRepository : CommonRepository<Departament, int>, IDepartamentRepository
    {
        public DepartamentRepository(ContextEF context) : base(context)
        {
        }
    }
}
