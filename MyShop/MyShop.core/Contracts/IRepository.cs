using System.Linq;
using MyShop.core.Models;

namespace MyShop.core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(T t);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}