namespace CollageApi.Data
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        public virtual ICollection<UserRoleMaping> UserRoleMap { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
