using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        //GET: Principal/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}