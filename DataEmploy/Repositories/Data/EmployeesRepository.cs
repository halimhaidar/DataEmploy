using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using DataEmploy.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Data;

namespace DataEmploy.Repositories.Data
{
    public class EmployeesRepository : GeneralRepository<AppDbContext, Employees, string>
    {
        public IConfiguration _configuration;
        private readonly AppDbContext context;
        public EmployeesRepository(IConfiguration configuration, AppDbContext context) : base (context)
        {
            _configuration = configuration;
            this.context = context;
        }

        DynamicParameters parameters = new DynamicParameters();

        public IEnumerable<Employees> Get()
        {
           throw new NotImplementedException();
        }

        public Employees Get(string key)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<EmployeesVM> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                var spName = "SP_EmployeesGet";
                var res = connection.Query<EmployeesVM>(spName, parameters, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public virtual int RegisterVM(RegisterVM registerVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
            {
                string generatedNIK = GenerateNIK();
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);

                var spName = "SP_EmployeesInsert";
                parameters = new DynamicParameters();
                parameters.Add("@NIK", generatedNIK);
                parameters.Add("@Password", passwordHash);
                parameters.Add("@FirstName", registerVM.FirstName);
                parameters.Add("@LastName", registerVM.LastName);
                parameters.Add("@Phone", registerVM.Phone);
                parameters.Add("@BirthDate", registerVM.BirthDate);
                parameters.Add("@Salary", registerVM.Salary);
                parameters.Add("@Email", registerVM.Email);
                parameters.Add("@Gender", registerVM.Gender);
                parameters.Add("@Manager_Id", registerVM.Manager_Id);
                parameters.Add("@Department_Id", registerVM.Department_Id);
                parameters.Add("@Role_Id", registerVM.Role_Id);
                var insert = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }
        public int Insert(Employees entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Employees employee)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:APISistemAbsensi"]))
            {
                var procName = "SP_EmployeesUpdate";
                parameters.Add("@NIK", employee.NIK);
                parameters.Add("@Name", employee.FirstName);
                parameters.Add("@Name", employee.LastName);
                parameters.Add("@Phone", employee.Phone);
                parameters.Add("@BirthDate", employee.BirthDate);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@Manager_Id", employee.Manager_Id);
                parameters.Add("@Department_Id", employee.Departement_Id);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }

        public int Delete(string key)
        {
            throw new NotImplementedException();
        }

        public string GenerateNIK()
        {
            var lastNIK = "";
            var newNIK = context.Employees.ToList().Count() + 1;

            if (newNIK >= 1 && newNIK <= 9)
            {
                lastNIK = "000" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 10 && newNIK <= 99)
            {
                lastNIK = "00" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 100 && newNIK <= 999)
            {
                lastNIK = "0" + Convert.ToString(newNIK);
            }

            DateTime dateTime = DateTime.UtcNow.Date;
            lastNIK = dateTime.ToString("yyyyddMM") + lastNIK;
            return lastNIK;
        }

        //private string GenerateNIK()
        //{
        //    var lastId = context.Employees.FromSqlRaw(
        //        "SELECT TOP 1 * " +
        //        "FROM Users " +
        //        "WHERE len(NIK) = 12 " +
        //        "ORDER BY RIGHT(NIK, 4) desc"
        //        ).ToList();
        //    int highestId = 0;
        //    if (lastId.Any())
        //    {
        //        var newId = lastId[0].NIK;
        //        newId = newId.Substring(newId.Length - 4);
        //        highestId = Convert.ToInt32(newId);
        //    }

        //    int increamentId = highestId + 1;
        //    string generatedNIK = increamentId.ToString().PadLeft(4, '0');
        //    DateTime today = DateTime.Today;
        //    var dateNow = today.ToString("yyyyddMM");
        //    generatedNIK = dateNow + generatedNIK;

        //    return generatedNIK;
        //}
    }
}
