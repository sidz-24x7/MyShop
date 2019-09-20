using MyShop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (null == items)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (null == tToUpdate)
            {
                throw new Exception(className + " Not Found");
            }
            else
            {
                //String id = tToUpdate.Id;
                tToUpdate = t;
                //tToUpdate.Id = id;
            }
        }

        public T Find(string Id)
        {
            T tToFind = items.Find(i => i.Id == Id);
            if (null == tToFind)
            {
                throw new Exception(className + " Not Found");
            }
            else
            {
                //String id = tToUpdate.Id;
                return tToFind;
                //tToUpdate.Id = id;
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(T t)
        {
            T tToDelete = items.Find(i => i.Id == t.Id);
            if (null == tToDelete)
            {
                throw new Exception(className + " Not Found");
            }
            else
            {
                //String id = tToUpdate.Id;
                items.Remove(tToDelete);
                //tToUpdate.Id = id;
            }
        }
    }
}
