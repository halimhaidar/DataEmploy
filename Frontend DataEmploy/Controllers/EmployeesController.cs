using Microsoft.AspNetCore.Mvc;

namespace Frontend_DataEmploy.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
