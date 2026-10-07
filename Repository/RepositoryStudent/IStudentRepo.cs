using CollageApi.Data;
using CollageApi.Repository.CommonRepository;

namespace CollageApi.Repository.RepositoryStudent
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
