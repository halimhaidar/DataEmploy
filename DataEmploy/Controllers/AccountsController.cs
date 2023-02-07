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
        public virtual ActionResult Insert(AccountsVM loginVM)
        {
            var insert = repository.Login(loginVM);
            if (insert != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Berhasil Login", Data = insert });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Gagal Login, username atau password salah.", Data = insert });
            }
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