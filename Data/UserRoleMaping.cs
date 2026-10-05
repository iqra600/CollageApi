namespace CollageApi.Data
{
    public class UserRoleMaping
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
