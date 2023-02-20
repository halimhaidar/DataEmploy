using Microsoft.AspNetCore.Mvc;

namespace Frontend_DataEmploy.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
