using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollageApi.Data
{
    public class Department
    {
       // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String DepartmentName { get; set; }
        public string? Description { get; set; }

        public List<Student>? Students { get; set; }
    }
}
