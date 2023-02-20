using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using DataEmploy.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class DepartmentsRepository : GeneralRepository<AppDbContext, Departments, int>
    {
        public IConfiguration _configuration;
        private readonly AppDbContext context;
        public DepartmentsRepository(IConfiguration configuration, AppDbContext context) : base(context)
        {
            _configuration = configuration;
            this.context = context;
        }
        DynamicParameters parameters = new DynamicParameters();

        public virtual IEnumerable<DepartmentsVM> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                var spName = "SP_DepartmentsGet";
                var res = connection.Query<DepartmentsVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return res;
            }
        }
    }
}
