using CollageApi.Data;
using System.Linq.Expressions;

namespace CollageApi.Repository.CommonRepository
{
    public interface ICommonRepo<T> where T : class
    {
        List<T> Students(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        T StudentByID(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        List<T> StudentByName(Expression<Func<T, bool>> filter);
        T CreateStudent(T std);

        T EditStudent(Expression<Func<T, bool>> filter, T std);

        T EditPartiallyStudent(Expression<Func<T, bool>> filter, T std);
        bool DeleteStudent(int id);
        T Detail(Expression<Func<T, bool>> filter, int id);
        T EntityRecord(Expression<Func<T,bool>> filter);
      //  T NewMethod(Expression<Func<T, bool>> filter, Expression<Func<T, bool>> filter2,out T a);

    }
}
