

using Microsoft.AspNetCore.Mvc;
using SchoolDb.Models;

namespace SchoolDb.Controllers
{
    public class TeacherPageController : Controller
    {
        private TeacherAPIController _api;
        // This is a Dependency Injection
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        [HttpGet]
        // This will return a list of first name and last name of all teachers
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.GetTeacherInfo();


            return View(Teachers);
        }
        [HttpGet]
        // This will search the table with the teacher id and return the all the details of that teacher
        public IActionResult Show(int id)
        {
            Teacher TeacherDetails = _api.TeacherInfo(id);

            return View(TeacherDetails);
        }
    }
}
