using System.Collections.Generic;

namespace TobaccoShop.Services
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
        void Load();
    }
}
