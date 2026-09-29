using CollageApi.Data;
using CollageApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollageApi.Models
{
    public class StudentImpliments : CommonRepo<Student>, IStudentRepo
    {
       // private CollageContext db;
         public StudentImpliments(CollageContext context):base(context)
        {
            //db=context;
        }

        public List<Student> studentslistwithEligbleWithAge()
        {
           return db.student.Where(x=>x.Age>=18).ToList();

        }

        // private readonly List<Student> studentlist;

        //public StudentImpliments()
        //{
        //    studentlist = new List<Student>() {

        //         new Student(){
        //         ID=1,
        //         Name="iqra",
        //         Email="iqra@gmail.com"
        //         },
        //          new Student(){
        //         ID=2,
        //         Name="hira",
        //         Email="hira@gmail.com"
        //         },
        //           new Student(){
        //         ID=3,
        //         Name="zain",
        //         Email="zain@gmail.com"
        //           },
        //            new Student(){
        //         ID=4,
        //         Name="hani",
        //         Email="hani@gmail.com"
        //         }



        //     };  
        //}
        //public Student CreateStudent(Student std)
        //{
        //    db.student.Add(std);
        //    db.SaveChanges();
        //    var student = db.student.First(x => x.ID == std.ID);

        //    return student;
        //}



        //public bool DeleteStudent(int id)
        //{
        //    var std = db.student.Find(id);
        //    if (std != null)
        //    {
        //        db.student.Remove(std);
        //        db.SaveChanges();
        //        return true;
        //    }

        //    return false ;

        //}

        //public Student Detail(int id)
        //{

        //    return db.student.FirstOrDefault(x => x.ID ==id);
        //}

        //public Student EditStudent(Student std)
        //{
        //  // int index= studentlist.IndexOf(std);

        // var IsExist=  db.student.Any(x=>x.ID==std.ID);
        //    if (IsExist )
        //    {
        //        db.Entry<Student>(std).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return std;


        //    }
        //    return null;
        //    //s.Name = std.Name;
        //    //s.Email = std.Email;
        //   // return std;
        //    //studentlist.Insert(index, s);
        //    //return studentlist.ElementAt(index);



        //}

        //public Student EditPartiallyStudent(Student std)
        //{
        //    // int index= studentlist.IndexOf(std);

        //    var IsExist = db.student.Any(x => x.ID == std.ID);
        //    if (IsExist)
        //    {
        //        db.SaveChanges();
        //        var paritialstudent = db.student.Find(std.ID);
        //        return paritialstudent;


        //    }
        //    return null;
        //    //s.Name = std.Name;
        //    //s.Email = std.Email;
        //    // return std;
        //    //studentlist.Insert(index, s);
        //    //return studentlist.ElementAt(index);



        //}



        //public Student StudentByID(int id)
        //{
        //    return db.student.Find(id);
        //}

        //public List<Student>  StudentByName(string Name)
        //{

        //        List<Student> students=db.student.Where(x => x.Name.Contains(Name)).ToList();
        //    return students;
        //}

        //public List<Student> Students()
        //{
        //   return db.student.ToList();

        //}







    }
}
