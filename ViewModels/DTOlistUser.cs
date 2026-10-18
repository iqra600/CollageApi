using CollageApi.Data;

namespace CollageApi.ViewModels
{
    public class DTOlistUser:Users
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public UserType UserType { get; set; } = new UserType();

        public int UserCount { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }



    }
}
