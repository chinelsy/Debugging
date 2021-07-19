using System.Collections.Generic;

namespace Day5.Interfaces
{
    public interface IService<TEntity> where TEntity: class
    {
        void Add(string key,  TEntity entity);
        void Update(string key, int index, TEntity entity);
        TEntity Get(string key,  int index);
        Dictionary<string, IList<TEntity>> GetAll();
        IEnumerable<TEntity> GetAll(string key);

    }
}   
