using CollageApi.Data;
using System.Linq.Expressions;

namespace CollageApi.Repository
{
    public interface ICommonRepo<T> where T : class
    {
        List<T> Students();
        T StudentByID(Expression<Func<T, bool>> filter);
        List<T> StudentByName(Expression<Func<T, bool>> filter, string Name);
        T CreateStudent(Expression<Func<T, bool>> filter, T std);

        T EditStudent(Expression<Func<T, bool>> filter, T std);

        T EditPartiallyStudent(Expression<Func<T, bool>> filter, T std);
        bool DeleteStudent(int id);
        T Detail(Expression<Func<T, bool>> filter, int id);
    }
}
