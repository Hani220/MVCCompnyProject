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

        Task <T?> GetAsync(int id);

        Task <IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);

        IQueryable<T> GetIQueryable();

        IEnumerable<T> GetIEnemerable();


         void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
