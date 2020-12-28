using Knowledge.Data.EF;
using Knowledge.Data.Repositories.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Data.Repositories
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {

    }
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
