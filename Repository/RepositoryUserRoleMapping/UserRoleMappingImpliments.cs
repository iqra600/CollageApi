using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollageApi.Repository.RepositoryStudent
{
    public class UserRoleMappingImpliments : CommonRepo<UserRoleMaping>, IUserRoleMappingRepo
    {
        // private CollageContext db;
        public UserRoleMappingImpliments(CollageContext context) : base(context)
        {
            //db=context;
        }

        //assign a role to a user.
        public UserRoleMaping assignUserRole(int userid, int roleid)
        {
            var asign = new UserRoleMaping()
            {
                RoleId = roleid,
                UserId = userid,
                isActive = true
            };
            db.UserRoleMapings.Add(asign);
            db.SaveChanges();
            return asign;

        }

        //completely unasgin user from a particular role.
        public bool DeleteAssignUserRole(int id)
        {
            var exist = db.UserRoleMapings.Find(id);

            if (exist != null)
            {
                db.UserRoleMapings.Remove(exist);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;

            }


        }
        //deactivate user from particular role not taking permanent access.
        public bool DeActiveAssignUserRole(int userid, int roleid)
        {
            var existingRecord = db.UserRoleMapings.FirstOrDefault(x => x.UserId == userid && x.RoleId == roleid);
            if (existingRecord != null)
            {
                existingRecord.isActive = false;
                db.Entry(existingRecord).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;

            }
        }
        //give access to user of particular role by activate this user for this role.
        public bool ActiveAssignUserRole(int userid, int roleid)
        {
            var existingRecord = db.UserRoleMapings.FirstOrDefault(x => x.UserId == userid && x.RoleId == roleid);
            if (existingRecord != null)
            {
                existingRecord.isActive = true;
                db.Entry(existingRecord).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}
