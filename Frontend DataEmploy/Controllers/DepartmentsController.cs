using Microsoft.AspNetCore.Mvc;

namespace Frontend_DataEmploy.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
