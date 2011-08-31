using System.Collections.Generic;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface IService<TEntity, TKey>
    {
        ICollection<TEntity> GetAll();
        TEntity Get(TKey id);
        void Add(TEntity newEntity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}