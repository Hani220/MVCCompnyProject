using Route.IKEA.DAL.Entities;
using Route.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.IKEA.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {

        T? Get(int id);

        IEnumerable<T> GetAll(bool withAsNoTracking = true);

        IQueryable<T> GetIQueryable();

        IEnumerable<T> GetIEnemerable();


        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);
    }
}
