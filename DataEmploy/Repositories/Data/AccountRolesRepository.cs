using DataEmploy.Contexts;
using DataEmploy.Models;
using Microsoft.EntityFrameworkCore;

namespace DataEmploy.Repositories.Data
{
    public class AccountRolesRepository : GeneralRepository<AppDbContext, AccountRoles, int>
    {
        public AccountRolesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
