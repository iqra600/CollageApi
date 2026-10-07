using AutoMapper;
using CollageApi.CommonResponse;
using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using CollageApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IMapper mapper;
        private CommonResponse<Role> response;
        private ICommonRepo<Role> repository;

        public RoleController(IMapper mapper,CommonResponse<Role> response,ICommonRepo<Role> repo)
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
               Role newRole= repository.CreateStudent(role);
                response.ModelData= newRole;
                response.Status = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }

            return response;

        }

    }
}
