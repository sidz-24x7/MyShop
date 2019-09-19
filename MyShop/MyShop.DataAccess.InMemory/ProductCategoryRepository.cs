using MyShop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> categories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            categories = cache["categories"] as List<ProductCategory>;
            if (null == categories)
            {
                categories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["categories"] = categories;
        }

        public void Insert(ProductCategory c)
        {
            categories.Add(c);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory categoryToUpdate = categories.Find(c => c.Id == category.Id);
            if (null == categoryToUpdate)
            {
                throw new Exception("Product Category not found");
            }
            else
            {
                categoryToUpdate = category;
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory category = categories.Find(c => c.Id == Id);
            if (null == category)
            {
                throw new Exception("Product Category not found");

            }
            else
            {
                return category;
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return categories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory categoryToDelete = categories.Find(c => c.Id == Id);
            if (null == categoryToDelete)
            {
                throw new Exception("Product Category not found");
            }
            else
            {
                categories.Remove(categoryToDelete);
            }
        }
    }
}
