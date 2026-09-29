using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace CollageApi.Data
{
    public class Student
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int? DepartmentID { get; set; }
        public virtual Department? Department { get; set; }

        //public string Password { get; set; }
        //// [Compare("Password")]
        //[Compare(nameof(Password))]
        //public string confirmPassword { get; set; }
        //// [DateValidate]

        //public DateTime Date { get; set; }
    }
}
