using AutoMapper;
using CollageApi.CommonResponse;
using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using CollageApi.Repository.RepositoryStudent;
using CollageApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ICommonRepo<Users> repository;
        private ICommonRepo<Role> repositoryRole;

        private IUserRoleMappingRepo roleMappingRepository;

        private CommonResponse<Users> response;
        private IMapper mapper;

        public UserController(ICommonRepo<Users> Repository,IMapper Mapper,CommonResponse<Users> Response, ICommonRepo<Role> repositoryrole, IUserRoleMappingRepo RoleMappingRepository)
        {
            repository=Repository;

            response=Response;
            mapper = Mapper;
            roleMappingRepository = RoleMappingRepository;
            repositoryRole = repositoryrole;
             
        }

        //action for assign role to a user.
        [HttpPost]
        [Route("AssignRoleToUser/{userId:int}/{roleId:int}")]
        public ActionResult assignRoleToUser(int userId, int roleId)
        {

            if (userId<=0 || roleId<=0)
            {
                return BadRequest();
            }
            //check the user is exist or not to assign a role.
          var user=  repository.StudentByID(x=>x.ID==userId);
            //check whether role is exist for assign to a user.
          var role = repositoryRole.StudentByID(x => x.ID == roleId);

            if (user!=null && role!=null) {

                var map = roleMappingRepository.assignUserRole(userId, roleId);

                if (map != null)
                {
                    return Ok(map);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return BadRequest();
            }
           
        }


        //unassign role to a user.
        [HttpDelete]
        [Route("RemoveRoleFromUser/{assignId:int}/{userid:int}")]
        public ActionResult<CommonRepo<UserRoleMaping>> RemoveassignRoleToUser(int assignId,int userid)
        {

            if (assignId <= 0)
            {
                return BadRequest();
            }
            UserRoleMaping userrolemap = roleMappingRepository.StudentByID(x => x.ID == assignId);


            if (userrolemap != null)
            {

                bool isMap = roleMappingRepository.DeleteAssignUserRole(assignId);

                if (isMap)
                {
                    response.Status = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"Role iS Taken By user with ID {userid}.";
                    return Ok(response);
                }
                else
                {

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                response.Message = "this user role mapping is not exist.";
                response.StatusCode = HttpStatusCode.NotFound;
                response.Status = false;
                return BadRequest();
            }

        }

        //unassign role to a user.
        [HttpPut]
        [Route("DeactiveRoleToUser/{UserId:int}/{RoleId:int}")]
        public ActionResult<CommonResponse<Users>> deActiveRoleFromUser(int UserId, int RoleId)
        {
            bool isDeactive = roleMappingRepository.DeActiveAssignUserRole(UserId,RoleId);
            if (isDeactive)
            {
               
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"the user with id {UserId} is deactivated for this role ID {RoleId}";

            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Status = false;
                response.Message = $"something wrong, still user is activated!";
            }
            return response;


        }

        [HttpPut]
        [Route("ActivateRoleForUser/{userid:int}/{roleid:int}")]
        public ActionResult<CommonResponse<Users>> ActiveRoleFromUser(int userid, int roleid)
        {
            bool isactive = roleMappingRepository.ActiveAssignUserRole(userid, roleid);
            if (isactive)
            {

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"the user with id {userid} is activated for this role ID {roleid}";

            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Status = false;
                response.Message = $"something wrong, still user is DeActivated!";
            }
            return response;


        }


        [HttpGet]
        [Route("UsersList")]
        public ActionResult<CommonResponse<Users>> users()
        {
            
           List<Users> users= repository.Students(x=>x.isDeleted==false,
               y=>y.UserType,
           //    z=>z.UserType.Users,
               x=>x.UserRolemap
           );
         

            if (users == null || users.Count <= 0)
            {
                response.Status = false;
                //thats becase this list does not have items so its not a badrequest from client.
                response.StatusCode = HttpStatusCode.NoContent;
                return response;

            }
            
            else
            {
                // var dto = new DTOlistUser();
                //List<DTOlistUser> dto= users.Select(x => new DTOlistUser()
                //   {

                //       ID = x.ID,
                //       Username = x.Username,
                //       UserType = x.UserType,
                //       CreatedDate = x.CreatedDate,
                //       ModifiedDate = x.ModifiedDate,
                //       isActive = x.isActive,
                //       isDeleted = x.isDeleted,
                //       UserCount = users.Count()

                //   }).ToList();

                //   response.ModelDataList =dto;
                response.ModelDataList = users;
                response.Status = true;
                //response.ModelDataList = users;
                response.StatusCode = HttpStatusCode.OK;
               
                return response;
            }

        }

        [HttpGet]
        [Route("User/{id:int}")]
        public ActionResult<CommonResponse<Users>> UserById(int id)
        {
            response.ModelData= repository.StudentByID(x => x.ID == id && x.isDeleted == false ,x=>x.UserRolemap);
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [HttpPost]
        [Route("userCreate")]
        public ActionResult<CommonResponse<Users>> CreateUsers(Users user)
        {
            ArgumentNullException.ThrowIfNull(user, $"form of user is {nameof(user)} null ");
            var existingusername = repository.StudentByID(x=>x.Username.ToLower().Equals(user.Username.ToLower()));
            if (existingusername==null)
            {
                user.isActive=true;
                user.isDeleted = false;
                user.CreatedDate = DateTime.Now;
                
                response.ModelData = repository.CreateStudent(user);
                response.Status = true;
                response.Message = "User is created succuessfully!";
                response.StatusCode = HttpStatusCode.Created;
                  
      

    }
            else
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "username is already exist";
            }
            return response;

        }
        [HttpPut]
        [Route("update/{id:int}", Name = "UpdateUser")]

        public ActionResult<CommonResponse<Users>> UserUpdate(int id, Users user)
        {

            if (id <= 0 || id != user.ID)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            else
            {
                Users result = repository .EditStudent(x => x.ID == id && x.isDeleted == false, user);
                response.ModelData = result;
                response.Status = true;
                response.StatusCode = HttpStatusCode.Accepted;

            }
            return response;
        }


        [HttpDelete]
        [Route("Delete/{id:int}", Name = "DeleteUser")]

        public ActionResult<CommonResponse<Users>> UserDelete(int id)
        {

            if (id <= 0)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            else
            {
                //this will delete complter reocrd from Database its a hard Delete.

                // bool result = repository.DeleteStudent(id);

                //this will mark entity as deleted true but not delteted from db just update coulmn from isdeleted false to isdeleted true.

                var existingRecord = repository.StudentByID(x => x.ID == id && x.isDeleted == false);
                existingRecord.isDeleted = true;
                repository.EditStudent(x => x.ID == id && x.isDeleted == false, existingRecord);
                response.Status = true;
                //resouse is deleted so thats why is on longer available.
                response.StatusCode = HttpStatusCode.Gone;


            }
            return response;
        }




    }
}
