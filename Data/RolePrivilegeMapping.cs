namespace CollageApi.Data
{
    public class RolePrivilegeMapping
    {
        public int ID { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int PrivilegeId { get; set; }
        public  virtual RolePrivilege privilege { get; set; }
        public bool isActive { get; set; }
    }
}
