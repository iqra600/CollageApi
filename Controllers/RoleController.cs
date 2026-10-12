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
        private CommonResponse<RoleDTO> response;
        private ICommonRepo<Role> repository;

        public RoleController(IMapper mapper, CommonResponse<RoleDTO> response, ICommonRepo<Role> repo)
        {
            this.mapper = mapper;
            this.response = response;
            repository = repo;
        }

        [HttpPost]
        [Route("CreateRole")]
        public ActionResult<CommonResponse<RoleDTO>> CreateRole(RoleDTO roleDto)
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
                

               // var newRole = repository.CreateStudent(role);
                RoleDTO roledto=mapper.Map<RoleDTO>(repository.CreateStudent(role));
                response.ModelData = roledto;
                response.Status = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }

            return response;

        }
        [HttpGet]
        [Route("RoleList", Name = "Roles")]
        public ActionResult<CommonResponse<RoleDTO>> GetAllrole()
        {

            var list = repository.Students(x=>x.isDeleted==false);
            if (list == null && list.Count < 1)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Status = false;

                return response;
            }
            else
            {
                response.ModelDataList =mapper.Map<List<RoleDTO>>(list);
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;
                return response;
            }

        }
        [HttpGet]
        [Route("{id:int}", Name = "Role")]
        public ActionResult<CommonResponse<RoleDTO>> RoleGetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();

            }
            Role existingRole = repository.StudentByID(x => x.ID == id && x.isDeleted==false);
            if (existingRole == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData =mapper.Map<RoleDTO>(existingRole);
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }

            return response;


        }
        [HttpGet]
        [Route("role/{id:int}")]
        public ActionResult<CommonResponse<RoleDTO>> RoleGetByRoleIdId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();

            }
            Role existingRole = repository.StudentByID(x => x.ID == id && x.isDeleted == false);
            if (existingRole == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData = mapper.Map<RoleDTO>(existingRole);
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }

            return response;


        }


        [HttpGet]
        [Route("{name:alpha}", Name = "RoleByName")]
        public ActionResult<CommonResponse<RoleDTO>> RoleGetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();

            }
            Role existingRole = repository.StudentByID(x => x.RoleName.Contains(name) && x.isDeleted == false);
            if (existingRole == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData =mapper.Map<RoleDTO>(existingRole);
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }

            return response;


        }
        [HttpPut]
        [Route("Update/{id:int}")]
        public ActionResult<CommonResponse<RoleDTO>> RoleUpdate(int id ,Role role)
        {

            if (role == null || role.ID <= 0 || id != role.ID)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Status = false;

            }
          
        
            else
            {
                var UpdatedRole = repository.EditStudent(x => x.ID == id && x.isDeleted == false, role);
                response.ModelData = mapper.Map<RoleDTO>(UpdatedRole);
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;

            }


            return response;

        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ActionResult<CommonResponse<RoleDTO>> RoleDelete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }
            else
            {
                var existingRole = repository.StudentByID(x => x.ID == id && x.isDeleted == false);
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
