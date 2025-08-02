
using MySql.Data.MySqlClient;

using Microsoft.AspNetCore.Mvc;
using SchoolDb.Models;
using System.Diagnostics;
using ZstdSharp.Unsafe;
using System.Reflection.Metadata.Ecma335;

namespace SchoolDb.Controllers
{
    [Route(template: "api/teacher")]
    [ApiController]

    public class TeacherAPIController : ControllerBase
    {
        SchoolDbContext School = new SchoolDbContext();
        [HttpGet(template: "getTeacherList")]
        public List<Teacher> GetTeacherInfo()
        {
            // Store it to a list
            List<Teacher> Example = new List<Teacher>();

            // Need to access info about teachers
            MySqlConnection Connection = School.AccessDatabase();

            Connection.Open();
            // connection to db

            MySqlCommand Command = Connection.CreateCommand();

            // creating query
            // read the search query
            Command.CommandText = "SELECT * FROM teachers";

            MySqlDataReader ReadResult = Command.ExecuteReader();

            while (ReadResult.Read())
            {
                Teacher Teacher = new Teacher();
                Teacher.TeacherId = Convert.ToInt32(ReadResult["teacherid"]);
                Teacher.TeacherFName = ReadResult["teacherfname"].ToString();
                Teacher.TeacherLName = ReadResult["teacherlname"].ToString();
                Teacher.EmployeeNumber = ReadResult["employeenumber"].ToString();
                Teacher.HireDate = DateTime.Parse(ReadResult["hiredate"].ToString());
                Teacher.Salary = decimal.Parse(ReadResult["salary"].ToString());

                Example.Add(Teacher);
            }
            ReadResult.Close(); // add it to the list
            Connection.Close(); // close the connection 

            return Example;
        }

        [HttpGet(template: "TeacherInfo/{TeachersId}")]
        public Teacher TeacherInfo(int TeachersId)
        {
            // Store it to a list
            Teacher Example = new Teacher();

            // Need to access info about teachers
            MySqlConnection Connection = School.AccessDatabase();

            Connection.Open();
            // connection to db

            MySqlCommand Command = Connection.CreateCommand();

            // creating query
            // read the search query
            Command.CommandText = $"SELECT * FROM teachers where teacherid = {TeachersId}";

            MySqlDataReader ReadResult = Command.ExecuteReader();

            if (ReadResult.Read())
            {
                Example.TeacherId = Convert.ToInt32(ReadResult["teacherid"]);
                Example.TeacherFName = ReadResult["teacherfname"].ToString();
                Example.TeacherLName = ReadResult["teacherlname"].ToString();
                Example.EmployeeNumber = ReadResult["employeenumber"].ToString();
                Example.HireDate = DateTime.Parse(ReadResult["hiredate"].ToString());
                Example.Salary = decimal.Parse(ReadResult["salary"].ToString());
            }
            ReadResult.Close(); // add it to the list
            Connection.Close(); // close the connection 

            return Example;
        }

        [HttpPost(template: "AddTeacher")]

        public int AddTeacher([FromBody] Teacher NewTeacher)
        {
            Debug.WriteLine($"Teacher TeacherFname {NewTeacher.TeacherFName}");
            Debug.WriteLine($"Teacher TeacherLname {NewTeacher.TeacherLName}");

            string query = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (0, @TeacherFname, @TeacherLname, @EmployeeNumber, CURRENT_DATE(), @Salary)";

            int TeacherId = -1;
            using (MySqlConnection Conn = School.AccessDatabase())
            {

                Conn.Open();

                MySqlCommand Command = Conn.CreateCommand();
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFName);
                Command.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLName);
                Command.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

                Command.ExecuteNonQuery();
                TeacherId = Convert.ToInt32(Command.LastInsertedId);
            }
            return TeacherId;
        }

        // <summary>
        // This code deletes the teacher if matching the id received from the database
        // </summary>
        // <param name="id">The primary Key of TeacherID</param>
        // <returns>The number of rows affected by delete</returns>
        // DELETE api/TeacherAPI/DeleteTeacher/11 ->1
        // </example>
        [HttpDelete(template: "DeleteTeacher/{id}")]

        public int DeleteTeacher(int id)
        {
            string query = "delete from teachers where teacherid=@id";
            int RowsAffected = 0;

            using (MySqlConnection Conn = _context.AccessDatabase())
            {
                Conn.Open();

                MySqlCommand Command = Conn.CreateCommand();
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@id", id);

                RowsAffected = Command.ExecuteNonQuery();
            }

            return RowsAffected;
        }
    }
}




