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
        private IConfiguration _config;
        private readonly AppDbContext context;
        public AccountsRepository(IConfiguration config, AppDbContext context  )
        {
            _config = config;
            this.context = context;
        }

        DynamicParameters parameters = new DynamicParameters();
        public UserTokenVM Login(AccountsVM accountVM)
        {
            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:API"]))
            {
                var spCheckPassword = "SP_UsersCheckPassword";
                parameters.Add("@Email", accountVM.Email);
                var userPassword = connection.QueryFirstOrDefault<Accounts>(spCheckPassword, parameters, commandType: CommandType.StoredProcedure);
                if (userPassword == null)
                {
                    return null;
                }

                bool verified = BCrypt.Net.BCrypt.Verify(accountVM.Password, userPassword.Password);
                if (!verified)
                {
                    return null;
                }

                parameters = new DynamicParameters();
                var spUserToken = "SP_UsersGetLoginTokenData";
                parameters.Add("@NIK", userPassword.NIK);
                var userToken = connection.QuerySingleOrDefault<UserTokenVM>(spUserToken, parameters, commandType: CommandType.StoredProcedure);
                if (userPassword == null)
                {
                    return null;
                }

                string token = GenerateJwtToken(userToken);
                userToken.Token = token;
                return userToken;
            }
        }

        private string GenerateJwtToken(UserTokenVM userToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("NIK", userToken.NIK),
                new Claim("Email", userToken.Email),
                new Claim("Role", userToken?.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
