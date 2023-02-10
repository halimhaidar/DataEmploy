using DataEmploy.Base;
using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRoles, AccountRolesRepository, int>
    {
        public AccountRolesController(AccountRolesRepository accountRolesRepository) : base(accountRolesRepository)
        {

        }
    }
}
