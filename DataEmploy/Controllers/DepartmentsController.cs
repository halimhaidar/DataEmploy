using DataEmploy.Base;
using DataEmploy.Models;
using DataEmploy.Repositories.Data;

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmensController : BaseController<Departments, DepartmentsRepository, int>
    {
        private readonly DepartmentsRepository repository;
        public DepartmensController(DepartmentsRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        [Route("All")]
        public virtual ActionResult Get()
        {
            var get = repository.GetAll();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.NotFound, message = get.Count() + " Data Ditemukan", Data = get });
            }
        }
    }

   
}
