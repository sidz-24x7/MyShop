using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (null == products)
            {
                products = new List<Product>();
            }
        }
        public void Commit() {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (null == productToUpdate)
            {
                throw new Exception("Product not found");

            } else
            {
                productToUpdate = product; 
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (null == product)
            {
                throw new Exception("Product not found");

            }
            else
            {
                return product;
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (null == productToDelete)
            {
                throw new Exception("Product not found");

            }
            else
            {
                products.Remove(productToDelete);
            }
        }
    }
}
