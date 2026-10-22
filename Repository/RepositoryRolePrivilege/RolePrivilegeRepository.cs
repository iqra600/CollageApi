using CollageApi.Data;
using CollageApi.Repository.CommonRepository;
using CollageApi.Repository.RepositoryStudent;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollageApi.Repository.RepositoryRolePrivilege
{
    public class RolePrivilegeRepository : CommonRepo<RolePrivilegeMapping>, IRolePriviegeRepository
    {
        public RolePrivilegeRepository(CollageContext context) : base(context)
        {

        }

        public bool ActivatePrivilegeForUser(int PrivilegeId, int RoleId)
        {
            var mapped = db.RolePrivilegeMappings.FirstOrDefault(x => x.RoleId == RoleId && x.ID == PrivilegeId);
           
            if (mapped != null)
            {
                mapped.isActive = true;
                db.Entry(mapped).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            return false;
            
            }

        public Role AssignPrivilege(int PrivilegeId, int RoleId)
        {
            //find the record if already exist if not then create new otherwise no same privilege assign again to same role.
            var mapped = db.RolePrivilegeMappings.FirstOrDefault(x => x.RoleId == RoleId && x.ID == PrivilegeId);
            Role role;
            //if mapped is null its meant that record is not exist so we can add record with provided data.
            if (mapped == null)
            {
                mapped = new RolePrivilegeMapping()
                {
                    PrivilegeId = PrivilegeId,
                    RoleId = RoleId,
                    isActive = true
                };

                db.RolePrivilegeMappings.Add(mapped);
                db.SaveChanges();
                 role = db.Roles.Include(x=>x.UserRoleMap).Include(x=>x.RolePrivilegesMapping).FirstOrDefault(x=>x.ID==RoleId);
               
            }
            else
            {
               role= db.Roles.Include(x => x.UserRoleMap).Include(x => x.RolePrivilegesMapping).FirstOrDefault(x => x.ID == RoleId);
            }
            return role;
          

        }

        public bool DeActivatePrivilegeFromRole(int PrivilegeId, int RoleId)
        {
            var mapped = db.RolePrivilegeMappings.FirstOrDefault(x => x.RoleId == RoleId && x.ID == PrivilegeId);
            if (mapped != null)
            {
                mapped.isActive = false;
                db.Entry(mapped).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else { 
                return false; 
            }
        }
        public bool RemovePrivilege(int PrivilegeId, int RoleId)
        {
            //find the record to be delteted is exist or not.
            var mapped = db.RolePrivilegeMappings.FirstOrDefault(x => x.RoleId == RoleId && x.ID == PrivilegeId);
            if (mapped != null)
            {
                db.RolePrivilegeMappings.Remove(mapped);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<RolePrivilege> RolePrivilegeByRoleID(int roleid)
        {
            db.RolePrivilegeMappings.Where(x => x.RoleId == roleid).Select(x=>x.privilege).ToList();
            return db.RolePrivilegeMappings.Where(x => x.RoleId == roleid).Select(x => x.privilege).ToList();


        }
    }
}
