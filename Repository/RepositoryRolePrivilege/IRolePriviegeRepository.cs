using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using System.Linq.Expressions;

namespace CollageApi.Repository.RepositoryRolePrivilege
{
    public interface IRolePriviegeRepository:ICommonRepo<RolePrivilegeMapping>
    {
        //assign privileges to Role
         Role AssignPrivilege(int privilegeID,int roleId);


        //unassign privileges from a role so that this role not able to access thse privileges.
        bool RemovePrivilege(int PrivilegeId,int RoleId);

        //deActivate privileges from user so that for specific perpose user is not able to use thse privileges.
        bool DeActivatePrivilegeFromRole(int PrivilegeId, int RoleId);

        //Activate privileges for users so he can prrform action according to their role on privileges.
        bool ActivatePrivilegeForUser(int PrivilegeId,int RoleId);
        List<RolePrivilege> RolePrivilegeByRoleID(int roleid);


    }
}
