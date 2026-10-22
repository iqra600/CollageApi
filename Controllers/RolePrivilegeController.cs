using AutoMapper;
using CollageApi.CommonResponse;
using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using CollageApi.Repository.RepositoryRolePrivilege;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using System.Net;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrivilegeController : ControllerBase
    {
        private IMapper
            mapper;
        private ICommonRepo<RolePrivilege> repository;
        private CommonResponse<RolePrivilege> response;
        private CommonResponse<Role> responseRole;

        private IRolePriviegeRepository repositoryRolePrivilegeMapping;
        private ICommonRepo<Role> repositoryRole;


        
        public RolePrivilegeController(
            IMapper mapper,
            ICommonRepo<RolePrivilege> repository,
            CommonResponse<RolePrivilege> response,
            IRolePriviegeRepository RepositoryRolePrivilegeMapping,
            ICommonRepo<Role> _RepositoryRole,
            CommonResponse<Role> responseRole)
        {
           this.mapper = mapper;
           this.repository = repository;
            this.response = response;
            repositoryRolePrivilegeMapping= RepositoryRolePrivilegeMapping;
            repositoryRole = _RepositoryRole;
            this.responseRole= responseRole;
           
                
        }

        #region Privilege Functionalities
        //method fro assign privilege to role
        [HttpPost]
        [Route("AssignPrivilegeToRole/{PrivilegeId:int}/{RoleId:int}")]
        public ActionResult<CommonResponse<Role>> Assign(int PrivilegeId,int RoleId)
        {
            if(PrivilegeId<=0 || RoleId <= 0)
            {

                return BadRequest();

            }
           var privilegeRecord= repository.StudentByID(x => x.ID == PrivilegeId);
          Role roleRecord=  repositoryRole.StudentByID(x => x.ID == RoleId);
            if(privilegeRecord!=null && roleRecord != null)
            {
                //if Privilege is mapped to a specific role then return this role that assign privilege.
              Role role=  repositoryRolePrivilegeMapping.AssignPrivilege(privilegeRecord.ID,roleRecord.ID);

                responseRole.Status = true;
                responseRole.ModelData = role;
                responseRole.StatusCode = HttpStatusCode.OK;
                responseRole.Message = "priviliege is assigned successfully!";
                return responseRole;

            }
            else
            {
                responseRole.Status = false;
                responseRole.StatusCode = HttpStatusCode.NotFound;
                responseRole.Message = "the role or privilege is not exist!";
                return responseRole;
            }
        }

        //method for delete privilege its mean taking permanent permision to access these privilages from the role.
        [HttpDelete]
        [Route("DeletePrivilegeFromRole/{PrivilegeId:int}/{RoleId:int}")]
        public ActionResult<CommonResponse<Role>> RemoveAssignRole(int PrivilegeId, int RoleId)
        {
            if (PrivilegeId <= 0 || RoleId <= 0)
            {

                return BadRequest();

            }
            var privilegeRecord = repository.StudentByID(x => x.ID == PrivilegeId);
            Role roleRecord = repositoryRole.StudentByID(x => x.ID == RoleId);
            if (privilegeRecord != null && roleRecord != null)
            {
                //if Privilege is mapped to a specific role then return this role that assign privilege.
               var result = repositoryRolePrivilegeMapping.RemovePrivilege(privilegeRecord.ID, roleRecord.ID);

                responseRole.Status = true;
               // responseRole.ModelData = role;
                responseRole.StatusCode = HttpStatusCode.OK;
                responseRole.Message = "priviliege is Deleted from role successfully!";
                return responseRole;

            }
            else
            {
                responseRole.Status = false;
                responseRole.StatusCode = HttpStatusCode.NotFound;
                responseRole.Message = "the role or privilege is not exist!";
                return responseRole;
            }
        }

        //deactive for temperory time privilege from role.
        [HttpPut]
        [Route("DeactivatePrivilege/{PrivilegeId:int}/{RoleId:int}")]
        public ActionResult<CommonResponse<Role>> DeactivePrivilege(int PrivilegeId, int RoleId)
        {
            if (PrivilegeId <= 0 || RoleId <= 0)
            {

                return BadRequest();

            }
            var privilegeRecord = repository.StudentByID(x => x.ID == PrivilegeId);
            Role roleRecord = repositoryRole.StudentByID(x => x.ID == RoleId);
            if (privilegeRecord != null && roleRecord != null)
            {
                //if Privilege is mapped to a specific role then return this role that assign privilege.
                var Result = repositoryRolePrivilegeMapping.DeActivatePrivilegeFromRole(privilegeRecord.ID, roleRecord.ID);

                responseRole.Status = true;
                //responseRole.ModelData = role;
                responseRole.StatusCode = HttpStatusCode.OK;
                responseRole.Message = "priviliege is Deactivated from Role successfully!";
                return responseRole;

            }
            else
            {
                responseRole.Status = false;
                responseRole.StatusCode = HttpStatusCode.NotFound;
                responseRole.Message = "the role or privilege is not exist!";
                return responseRole;
            }
        }

        [HttpPut]
        [Route("ActivatePrivilege/{PrivilegeId:int}/{RoleId:int}")]
        public ActionResult<CommonResponse<Role>> ActivatePrivilegeFroRole(int PrivilegeId, int RoleId)
        {
            if (PrivilegeId <= 0 || RoleId <= 0)
            {

                return BadRequest();

            }
            var privilegeRecord = repository.StudentByID(x => x.ID == PrivilegeId);
            Role roleRecord = repositoryRole.StudentByID(x => x.ID == RoleId);
           
            if (privilegeRecord != null && roleRecord != null)
            {
                //if Privilege is mapped to a specific role then return this role that assign privilege.
                var result = repositoryRolePrivilegeMapping.ActivatePrivilegeForUser(privilegeRecord.ID, roleRecord.ID);

                responseRole.Status = true;
               // responseRole.ModelData = role;
                responseRole.StatusCode = HttpStatusCode.OK;
                responseRole.Message = "priviliege is activated for Role successfully!";
                return responseRole;

            }
            else
            {
                responseRole.Status = false;
                responseRole.StatusCode = HttpStatusCode.NotFound;
                responseRole.Message = "the role or privilege is not exist!";
                return responseRole;
            }
        }



        #endregion

        #region Privileges Crud

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommonResponse<RolePrivilege>> RolePrivileges()
        {
         var list=   repository.Students(x=>x.isDeleted==false);
            if(list==null || list.Count <= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Status = false;

            }
            else
            {
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.ModelDataList = list;

            }
            return response;


        }
        
        [HttpGet]
        [Route("{id:int}",Name ="RolePrivilegeById")]
        public ActionResult<CommonResponse<RolePrivilege>> PrivilegeByID(int id)
        {

            if (id <= 0)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var existingRecord= repository.StudentByID(x=>x.ID==id && x.isDeleted==false);
            if (existingRecord == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelData = existingRecord;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                
            }
            return response;
        }
        [HttpGet]
        [Route("{name:alpha}", Name = "RolePrivilegeByName")]

        public ActionResult<CommonResponse<RolePrivilege>> PrivilegeByName(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var existingRecord = repository.StudentByName(x => x.RolePrivilegeName.Contains( name) && x.isDeleted==false);
            if (existingRecord == null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelDataList = existingRecord;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            return response;
        }
        [HttpGet]
        [Route("Role/{roleid:int}", Name = "RolePrivilegeByRoleId")]

        public ActionResult<CommonResponse<RolePrivilege>> PrivilegeByRoleId(int roleid)
        {

            if (roleid <= 0)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var list = repositoryRolePrivilegeMapping.RolePrivilegeByRoleID(roleid);
            if (list == null || list.Count <= 0)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.ModelDataList = list;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            return response;
        }

        [HttpPost]
        public ActionResult<CommonResponse<RolePrivilege>> CreatePrivilege(RolePrivilege privilege)
        {
            if (privilege==null)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            if (!ModelState.IsValid)
            {

             
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;


            }
            else{

                var NewRecord = repository.CreateStudent(privilege);
                response.ModelData = NewRecord;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
            }



            return response;
        }

        [HttpPut]
        [Route("update/{id:int}", Name = "UpdatePrivilege")]

        public ActionResult<CommonResponse<RolePrivilege>> PrivilegeUpdate(int id,RolePrivilege privilege)
        {

            if (id <= 0 || id!=privilege.ID)
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            else
            {
                RolePrivilege result=repository.EditStudent(x=>x.ID==id && x.isDeleted==false,privilege);
                response.ModelData = result;
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;

            }
            return response;
        }


        [HttpDelete]
        [Route("Delete/{id:int}", Name = "DeletePrivilege")]

        public ActionResult<CommonResponse<RolePrivilege>> PrivilegeDelete(int id)
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

                var existingRecord = repository.StudentByID(x => x.ID == id && x.isDeleted==false);
                existingRecord.isDeleted = true;
                repository.EditStudent(x => x.ID == id && x.isDeleted==false,existingRecord );
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;


            }
            return response;
        }
        #endregion 

        
    }
}
