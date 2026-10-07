using AutoMapper;
using CollageApi.CommonResponse;
using CollageApi.Data;
using CollageApi.Repository;
using CollageApi.Repository.RepositoryStudent;
using CollageApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableCors("AllowtLocaolHost")] //it will overide application level or default  cors if applied in program.cs file.
    [Authorize]
    public class StudentController : ControllerBase
    {
        private IStudentRepo Studentlist;
        private IMapper mapper;
        //private CommonResponse<Student> response;

       // public ICommonRepo<Student> Studentlist;

        private readonly ILogger<StudentController> ilogger;
        //public StudentController(ILogger<StudentController> _ilogger, IStudentRepo StudentRepo)
        //{
        //    ilogger = _ilogger;
        //    Studentlist = StudentRepo;
        //}


        public StudentController(IMapper _mapper, IStudentRepo studentsrepo)
        {
            Studentlist = studentsrepo;
            mapper = _mapper;
           // response = new CommonResponse<Student>();
          //  Studentlist = repo;
        }

        [HttpGet]
        public ActionResult<CommonResponse<ViewModelStudent>> Students()
        {

            var response = new CommonResponse<ViewModelStudent>();

            try
            {



                if (Studentlist.Students() == null || Studentlist.Students().Count < 1)
                {
                    //ilogger.LogTrace("msg from trace.");
                    //ilogger.LogInformation("msg from info.");
                    //ilogger.LogWarning("msg from warnig.");
                    //ilogger.LogError("msg from error.");
                    //ilogger.LogCritical("msg from critical.");

                    return NotFound();

                }
                var data = Studentlist.Students();
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;

                //var dtoStudent=   students.Select(x => new ViewModelStudent()
                //   {
                //       Name=x.Name,
                //       Age=x.Age,
                //       isEligible=x.Age>=18?true:false,
                //       Email=x.Email,
                //       ID=x.ID

                //   }).ToList();
                // response.ModelDataList = mapper.Map<List<ViewModelStudent>>(response.ModelDataList);
                var dtoStudent = mapper.Map<List<ViewModelStudent>>(data);
                response.ModelDataList = dtoStudent;

                return response;
                // return Ok(Studentlist.Students());
            }
            catch (Exception e) { 
                response.Errors.Add(e.Message);
                response.StatusCode=HttpStatusCode.InternalServerError;
                response.Status = false;
                return response;
            }

        }

        [HttpGet]
        [Route("{id:int}", Name = "StudentByID")]
        // [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> StudentByID(int id)
        {
            //validating the id from the client site.
            if (id <= 0)
            {
                return BadRequest();//400
            }
            if (Studentlist.StudentByID(x=>x.ID==id) == null)
            {
                return NotFound();//404
            }


            var student= Studentlist.StudentByID(x => x.ID == id);
            //validating the id given by client is in database or not.
            if (student==null)
            {
                //the resource is not found mean the given id is not in database.
                return NotFound();
            }
            return student;

        }


        [HttpGet]
        [Route("{name:alpha}")]
        public ActionResult<List<Student>> StudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
           List<Student> students=Studentlist.StudentByName(x => x.Name.ToLower().Contains(name.ToLower()),name);
            if (students == null || students.Count<1 )
            {
                return NotFound();

            }
            return students;

        }
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"the request is bad!");
            }

          //  var studentToBeDeleete = Studentlist.StudentByID(id);

            //if (studentToBeDeleete == null)
            //{
            //    return NotFound($"requested resourse not found!");
            //}


            bool isDeleted = Studentlist.DeleteStudent(id);
            if (isDeleted)
            {
                return Ok(true);

            }
            else
            {
                return NotFound();
               // return StatusCode(500, "student could not be Deleted!");
            }

        }
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult UpdateStudent(Student s)
        {
            if (s == null || s.ID <= 0)
            {
                return BadRequest();
            }
            else
            {
                var student = Studentlist.EditStudent(x=>x.ID==s.ID,s);
                if (student!=null) {
                    return NoContent();
                }
               
               
                return NotFound();

            }


            //return Studentlist.EditStudent(s);
            return NoContent();

        }
        [HttpPatch]
        [Route("UpdatePartially/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [DisableCors]//this attribute disable any cors orign its mean if any cors apply on this controller or application leve ,so onto this method it will not working.
        public ActionResult<Student> editPartial(int id, JsonPatchDocument<Student> document)
        {
            if (document == null || id <= 0)
            {
                return BadRequest();
            }
            var student = Studentlist.StudentByID(x=>x.ID==id);
            if (student == null)
            {
                return NotFound();
            }

            else
            {
                Student s = new Student()
                {
                    ID = student.ID,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age,
                    //Password = student.Password,
                    //confirmPassword = student.confirmPassword,
                    //Date = student.Date

                };

                document.ApplyTo(s, ModelState);
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                else
                {

                    student.Name = s.Name;
                    student.Email = s.Email;
                    student.Age = s.Age;
                  return  Studentlist.EditPartiallyStudent(x=>x.ID==id,student);

                    //student.Password = s.Password;
                    //student.confirmPassword = s.confirmPassword;
                    //student.Date = s.Date;
                     
                   // return NoContent();

                }




            }


            //return Studentlist.EditStudent(s);
            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Student> Create(Student s)
        {

            Studentlist.CreateStudent(s);

            return CreatedAtRoute("StudentByID", new { id = s.ID }, s);
            // return Studentlist.CreateStudent(s);

        }

        [HttpGet]
        [Route("geteligiblelistofstudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Student>> getEligibleStudents()
        {


            return Ok( Studentlist.studentslistwithEligbleWithAge());
        }
    }
}
