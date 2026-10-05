namespace CollageApi.Data
{
    public class UserType
    {
        public int ID { get; set; }
        public string UserTypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
