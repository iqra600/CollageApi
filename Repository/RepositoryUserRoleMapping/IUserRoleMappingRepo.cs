using CollageApi.Data;
using CollageApi.Repository.CommonRepository;

namespace CollageApi.Repository.RepositoryStudent
{
    public interface IUserRoleMappingRepo: ICommonRepo<UserRoleMaping>
    {

        public UserRoleMaping assignUserRole(int userid, int roleid);
        public bool DeleteAssignUserRole(int id);
        public bool DeActiveAssignUserRole(int userid, int roleid);
        public bool ActiveAssignUserRole(int userid, int roleid);


    }
}
