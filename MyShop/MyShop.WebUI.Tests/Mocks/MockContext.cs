using MyShop.core.Contracts;
using MyShop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            items = new List<T>();
        }  

    public void Commit()
    {
        return;
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
            tToUpdate = t;
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
            return tToFind;
        }
    }

    public IQueryable<T> Collection()
    {
        return items.AsQueryable();
    }

    public void Delete(String Id)
    {
        T tToDelete = items.Find(i => i.Id == Id);
        if (null == tToDelete)
        {
            throw new Exception(className + " Not Found");
        }
        else
        {
            items.Remove(tToDelete);
        }
    }
}
}

