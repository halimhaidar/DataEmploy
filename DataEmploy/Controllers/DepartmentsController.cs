using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentsRepository repository;

        public DepartmentsController(DepartmentsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public virtual ActionResult Insert(Departments departments)
        {
            var insert = repository.Insert(departments);
            if (insert >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dimasukkan", Data = insert });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Memasukkan Data", Data = insert });
            }
        }
    }
}
