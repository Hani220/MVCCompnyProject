using Route.IKEA.DAL.Entities;
using Route.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Route.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T>(ApplicationDbContext dbContext):IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext = dbContext;
      

        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _dbContext.Set<T>().Where(x => !x.IsDeleted).AsNoTracking().ToList();

            return _dbContext.Set<T>().Where(x => !x.IsDeleted).ToList();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetIEnemerable()
        {
            return _dbContext.Set<T>();
        }

        public T? Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

       
    }
}
