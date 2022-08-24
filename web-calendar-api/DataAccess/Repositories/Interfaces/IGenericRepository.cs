using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TID, TEntity>
    {
        public TEntity Add(TEntity entity);
        public TEntity Update(TEntity entity);
        public void Delete(TEntity entity);
        public TEntity GetById(TID id);
        public IEnumerable<TEntity> GetAll();
    }
}