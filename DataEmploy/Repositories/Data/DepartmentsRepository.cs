using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class DepartmentsRepository : GeneralRepository<AppDbContext, Departments, int>
    {
        public DepartmentsRepository(AppDbContext context) : base(context)
        {

        }
    }
}
