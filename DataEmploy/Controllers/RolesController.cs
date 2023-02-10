using DataEmploy.Base;
using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Roles, RolesRepository, int>
    {
        public RolesController(RolesRepository rolesRepository) : base(rolesRepository)
        {

        }
    }
}
