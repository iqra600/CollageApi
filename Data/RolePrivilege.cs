namespace CollageApi.Data
{
    public class RolePrivilege
    {
        public int ID { get; set; }
        public string RolePrivilegeName { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
