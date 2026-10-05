using System.ComponentModel.DataAnnotations;

namespace CollageApi.ViewModels
{
    public class RoleDTO
    {
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
