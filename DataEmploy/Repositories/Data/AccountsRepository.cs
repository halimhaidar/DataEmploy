using Dapper;
using DataEmploy.Contexts;
using DataEmploy.Models;
using DataEmploy.Repositories.Interface;
using DataEmploy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataEmploy.Repositories.Data
{
    public class AccountsRepository
    {
        private readonly AppDbContext context;
        private IConfiguration _config;
        public AccountsRepository(AppDbContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }


        public async Task<string> Login(AccountsVM accountsVM)
        {
            var user = await context.AccountRoles
                .Where(ar => ar.Accounts.Employees.Email == accountsVM.Email
                && ar.Accounts.Password == accountsVM.Password)
                .Include(r => r.Roles).Include(e => e.Accounts.Employees)
                .FirstOrDefaultAsync();

            if (accountsVM.Password == null)
            {
                return "400";
            }
            else if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                    new Claim("Email", user.Accounts.Employees.Email),
                    new Claim("roles", user.Roles.Name)
                   };

                var token = new JwtSecurityToken(_config["JWT:Issuer"],
                    _config["JWT:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            else
            {
                return "404";
            }
        }

        //public string Generate(AccountVM accountVM)
        //{
        //	var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        //	var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //	var claims = new[]
        //	{
        //		new Claim(ClaimTypes.Email, account.Employee.Email)
        //	};

        //	var token = new JwtSecurityToken(_config["JWT:Issuer"],
        //		_config["JWT:Audience"],
        //		claims,
        //		expires: DateTime.Now.AddMinutes(15),
        //		signingCredentials: credentials);

        //	return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //public async Task<Account> Authenticate(AccountVM accountVM)
        //{
        //	var currentUser = await context.Accounts.FirstOrDefaultAsync(
        //		a => a.Employee.Email.ToLower() == account.Employee.Email.ToLower()
        //		&&
        //		a.Password == account.Password);
        //	if (currentUser != null)
        //	{
        //		return currentUser;
        //	}
        //	return null;
        //}
    }

    //public class AccountsRepository
    //{
    //    public IConfiguration _configuration;
    //    private readonly AppDbContext context;
    //    public AccountsRepository(IConfiguration configuration, AppDbContext context)
    //    {
    //        _configuration = configuration;
    //        this.context = context;
    //    }
    //    DynamicParameters parameters = new DynamicParameters();

    //    public string Login(AccountsVM accountsVM)
    //    {
    //        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API"]))
    //        {
    //            var spCheckEmail = "SP_AccountCheckEmail";
    //            parameters.Add("@email", accountsVM.Email);
    //            parameters.Add("@password", accountsVM.Password);
    //            var accountEmail = connection.QueryFirstOrDefault<AccountsVM>(spCheckEmail, parameters, commandType: CommandType.StoredProcedure);
    //            if (accountEmail != null)
    //            {
    //                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
    //                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //                var claims = new[] {
    //                                new Claim("Email", accountEmail.Email)

    //                               };

    //                var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
    //                    _configuration["JWT:Audience"],
    //                    claims,
    //                    expires: DateTime.Now.AddMinutes(15),
    //                    signingCredentials: credentials);

    //                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    //                return jwt;
    //            }
    //            else
    //            {
    //                return "400";
    //            }
    //        }

    //    }
    //}
}
