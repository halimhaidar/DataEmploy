using DataEmploy.Base;
using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using DataEmploy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employees, EmployeesRepository, string>
    {
        private readonly EmployeesRepository repository;
        public EmployeesController(EmployeesRepository repository) : base(repository)
        {
            this.repository = repository;
        }
       /* [HttpGet]
        public virtual ActionResult Get()
        {
            var get = repository.Get();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.NotFound, message = get.Count() + " Data Ditemukan", Data = get });
            }
        }*/

        [HttpPost]
        [Route("Register")]
        public virtual ActionResult Insert(RegisterVM registerVM)
        {
            var insert = repository.RegisterVM(registerVM);
            if (insert >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dimasukkan", Data = insert });
            }
            else if (insert == -11)
            {
                return StatusCode(500, new { status = HttpStatusCode.OK, message = "Gagal Memasukkan Data. Username sudah digunakan.", Data = insert });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Memasukkan Data", Data = insert });
            }
        }

    }
}
