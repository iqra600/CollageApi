using AutoMapper;
using CollageApi.CommonResponse;
using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using CollageApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IMapper mapper;
        private CommonResponse<Role> response;
        private ICommonRepo<Role> repository;

        public RoleController(IMapper mapper, CommonResponse<Role> response, ICommonRepo<Role> repo)
        {
            this.mapper = mapper;
            this.response = response;
            repository = repo;
        }

        [HttpPost]
        [Route("CreateRole")]
        public ActionResult<CommonResponse<Role>> CreateRole(RoleDTO roleDto)
        {
            if (roleDto == null)
            {
                //return BadRequest();
                response.Status = false;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            }
            else
            {
                Role role = mapper.Map<Role>(roleDto);
                role.isDeleted = false;
                role.CreatedDate = DateTime.Now;
                role.isActive = true;
                Role newRole = repository.CreateStudent(role);
                response.ModelData = newRole;
                response.Status = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }

            return response;

        }
        [HttpGet]
        [Route("RoleList", Name = "Roles")]
        public ActionResult<CommonResponse<Role>> GetAllrole()
        {

            var list = repository.Students();
            if (list == null && list.Count < 1)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Status = false;

                return response;
            }
            else
            {
                response.ModelDataList = list;
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;
                return response;
            }

        }
        [HttpGet]
        [Route("{id:int}", Name = "Role")]
        public ActionResult<CommonResponse<Role>> RoleGetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();

            }
            var existingRole = repository.StudentByID(x => x.ID == id);
            if (existingRole == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData = existingRole;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }

            return response;


        }
        [HttpGet]
        [Route("{name:alpha}", Name = "RoleByName")]
        public ActionResult<CommonResponse<Role>> RoleGetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();

            }
            var existingRole = repository.StudentByID(x => x.RoleName.Contains(name));
            if (existingRole == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData = existingRole;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }

            return response;


        }
        [HttpPut]
        [Route("Update")]
        public ActionResult<CommonResponse<Role>> RoleUpdate(Role role)
        {

            if (role == null && role.ID <= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Status = false;

            }
            var roleExisting = repository.StudentByID(x => x.ID == role.ID);

            if (roleExisting == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Status = false;


            }
            else
            {
                var UpdatedRole = repository.EditStudent(x => x.ID == role.ID, role);
                response.ModelData = UpdatedRole;
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;

            }


            return response;

        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ActionResult<CommonResponse<Role>> RoleDelete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }
            else
            {
                var existingRole = repository.StudentByID(x => x.ID == id);
                if (existingRole == null)
                {
                    return NotFound();


                }
                else
                {
                   var isDeleted= repository.DeleteStudent(existingRole.ID);
                    response.StatusCode= isDeleted==true?HttpStatusCode.OK:HttpStatusCode.InternalServerError;
                    response.Status = isDeleted;


                }

            }
            return response;
        }
    } 
}
