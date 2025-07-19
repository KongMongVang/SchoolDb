

using Microsoft.AspNetCore.Mvc;
using SchoolDb.Models;

namespace SchoolDb.Controllers
{
    public class TeacherPageController : Controller
    {
        private TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult List()
        {
        List<Teacher> Teachers = _api.GetTeacherInfo();
    

            return View(Teachers);
        }
    }
}
