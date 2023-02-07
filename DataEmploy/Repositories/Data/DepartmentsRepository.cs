using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class DepartmentsRepository : IRepository<Departments, int>
    {
        public IConfiguration _configuration;
        private readonly AppDbContext context;
        public DepartmentsRepository(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        DynamicParameters parameters = new DynamicParameters();

        public IEnumerable<Departments> Get()
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:APISistemAbsensi"]))
            {
                var spName = "SP_DepartmentsGetAll";
                var res = connection.Query<Departments>(spName, commandType: CommandType.StoredProcedure);
                return res;
                //throw new System.NotImplementedException();
            }
        }

        public Departments Get(int key)
        {
            throw new NotImplementedException();
        }

        public int Insert(Departments departments)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                var spName = "SP_DepartmentsInsert";
                parameters.Add("@DepartmentName", departments.Name);
                parameters.Add("@Manager_Id", departments.Manager_Id);
                var insert = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Update(Departments department)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                var spName = "SP_DepartmentsUpdate";
                parameters.Add("@Id", department.Id);
                parameters.Add("@DepartmentName", department.Name);
                parameters.Add("@Manager_Id", department.Manager_Id);
                var update = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }

        public int Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}
