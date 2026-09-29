using CollageApi.Repository;

namespace CollageApi.Data
{
    public interface IStudentRepo: ICommonRepo<Student>
    {
        List<Student> studentslistwithEligbleWithAge();
        // List<Student> Students();
        // Student StudentByID(int id);
        //List<Student>  StudentByName(string Name);
        // Student CreateStudent(Student std);

        // Student EditStudent(Student std);

        //Student EditPartiallyStudent(Student std);
        // bool DeleteStudent(int id);
        // Student Detail(int id);
    }
}
