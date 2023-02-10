
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class RolesRepository : GeneralRepository<AppDbContext, Roles, int>
    {
        public RolesRepository(AppDbContext context) : base(context)
        {

        }
    }
}

