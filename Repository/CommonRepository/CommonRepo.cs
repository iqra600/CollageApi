using CollageApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollageApi.Repository.CommonRepository
{
    public class CommonRepo<T>:ICommonRepo<T> where T : class
    {
       
            protected CollageContext db;
        private DbSet<T> dbEntity;

        public CommonRepo(CollageContext context)
            {
             db = context;
            dbEntity = context.Set<T>();
            }
 
            public T CreateStudent(T std)
            {
            dbEntity.Add(std);
                db.SaveChanges();
              //  var student = dbEntity.FirstOrDefault(filter);

                return std;
            }



            public bool DeleteStudent(int id)
            {
                var std = dbEntity.Find(id);
                if (std != null)
                {
                dbEntity.Remove(std);
                    db.SaveChanges();
                    return true;
                }

                return false;

            }

            public T Detail(Expression<Func<T,bool>> filter,int id)
            {

                return dbEntity.FirstOrDefault(filter);
            }

            public T EditStudent(Expression<Func<T, bool>> filter, T std)
            {
                // int index= studentlist.IndexOf(std);

                var IsExist = dbEntity.Any(filter);
                if (IsExist)
                {
                    db.Entry<T>(std).State = EntityState.Modified;
                    db.SaveChanges();
                    return std;


                }
                return null;
             
            }

            public T EditPartiallyStudent(Expression<Func<T, bool>> filter, T std)
            {
            // int index= studentlist.IndexOf(std);

            // var IsExist = dbEntity.Any(x => x.ID == std.ID);
            var IsExist = StudentByID(filter)!=null?true:false;
            if (IsExist)
                {
                    db.SaveChanges();
                    var paritialstudent = dbEntity.FirstOrDefault(filter);
                    return paritialstudent;


                }
                return null;
               



            }



            public T StudentByID(Expression<Func<T,bool>> filter)
            {
                return dbEntity.FirstOrDefault(filter);
            }

            public List<T> StudentByName(Expression<Func<T, bool>> filter,string name)
            {

                List<T> students = dbEntity.Where(filter).ToList();
                return students;
            }

            public List<T> Students()
            {
                return dbEntity.ToList();

            }

      

       

       

       
    }
}
