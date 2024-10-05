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
      

        public async Task <IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(x => !x.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetIEnemerable()
        {
            return _dbContext.Set<T>();
        }

        public async Task <T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Add(T entity) =>  _dbContext.Set<T>().Add(entity);





        public void Update(T entity) => _dbContext.Set<T>().Update(entity);




        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
          
        }

       
    }
}
