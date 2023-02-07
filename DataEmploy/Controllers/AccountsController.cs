using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using DataEmploy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsRepository repository;
        public AccountsController(AccountsRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(AccountsVM accountsVM)
        {
            var login = await repository.Login(accountsVM);
            if (login != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Berhasil", Data = login });
            }
            else if (login == "400")
            {
                return StatusCode(400, new { status = HttpStatusCode.NotFound, message = "Password Salah" });
            }
            return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Login Gagal" });
        }
    }

    //[Route("api/[controller]")]
    //[ApiController]
    //public class AccountsController : ControllerBase
    //{
    //    private readonly AccountsRepository repository;

    //    public AccountsController(AccountsRepository repository)
    //    {
    //        this.repository = repository;
    //    }

    //    [HttpPost]
    //    public virtual ActionResult Insert(AccountsVM accountsVM)
    //    {
    //        var insert = repository.Login(accountsVM);
    //        if (insert != null)
    //        {
    //            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Berhasil Login", Data = insert });
    //        }
    //        else
    //        {
    //            return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Login, username atau password salah.", Data = insert });
    //        }
    //    }
    //}
}