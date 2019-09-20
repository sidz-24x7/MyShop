using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.core.Models;
using MyShop.core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategories;

        public ProductManagerController(){

            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();

        }
        
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            Product productToUpdate = context.Find(Id);
            if(null == productToUpdate)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = productToUpdate;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, String Id)
        {
            Product productToUpdate = context.Find(Id);
            if (null == productToUpdate)
            {
                return HttpNotFound();
            }
            else if(!ModelState.IsValid)
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = productToUpdate;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
            else
            {
                productToUpdate.Category = product.Category;
                productToUpdate.Description = product.Description;
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Image = product.Image;

                //context.Update(productToUpdate);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(String Id)
        {
            Product productToDelete = context.Find(Id);
            if (null == productToDelete)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            Product productToDelete = context.Find(Id);
            if (null == productToDelete)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(productToDelete);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}