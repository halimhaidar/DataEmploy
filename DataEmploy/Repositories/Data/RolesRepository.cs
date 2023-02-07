using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class RolesRepository : IRepository<Roles, int>
    {
        public IConfiguration _configuration;
        private readonly AppDbContext context;
        public RolesRepository(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            this.context = context;
        }

        DynamicParameters parameters = new DynamicParameters();
        public int Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> Get()
        {
            throw new NotImplementedException();
        }

        public Roles Get(int key)
        {
            throw new NotImplementedException();
        }

        public int Insert(Roles roles)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                var spName = "SP_RolesInsert";
                parameters.Add("@RolesName", roles.Name);
                var insert = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Update(Roles entity)
        {
            throw new NotImplementedException();
        }
    }
}
