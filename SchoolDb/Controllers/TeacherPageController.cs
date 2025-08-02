

using Google.Protobuf.WellKnownTypes;
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
        // GET: Teacher/New -> A webpage that asks the user for the new teacher information
        [HttpGet]

        public IActionResult New()
        {
            return View();
        }

        // POST: TeacherPage/New
        // Headers:
        // application/x-www.form-unlocked
        // Request Body: &TeacherFname={TeacherFname}&TeacherFname={TeacherLname}
        // Add -> Adds teacher and directs to List.cshtml
        [HttpPost]

        public IActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            int TeacherId = _api.AddTeacher(NewTeacher);

            // Directs to /TeacherPage/List.cshtml
            return RedirectToAction("Show", new { id = TeacherId });
        }

        // GET: /TeacherPage/DeleteConfirm/{id} -> A webpage that asks a user if they want to delete this article
        [HttpGet]

        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.TeacherInfo(id);

            return View(SelectedTeacher);
        }

        // POST: /TeacherPage/Delete/{id} -> Deletes the article and returns to the List.cshtml

        [HttpPost]

        public IActionResult Delete(int id)
        {
            _api.DeleteTeacher(id);

            return RedirectToAction("List");
        }

    }
    
}
