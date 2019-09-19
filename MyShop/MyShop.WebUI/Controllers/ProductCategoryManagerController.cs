using MyShop.core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collection().ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            ProductCategory category1 = new ProductCategory();
            return View(category1);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory category1)
        {
            if (!ModelState.IsValid)
            {
                return View(category1);
            }
            else
            {
                context.Insert(category1);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            ProductCategory categoryToUpdate = context.Find(Id);
            if (null == categoryToUpdate)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToUpdate);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory category1, String Id)
        {
            ProductCategory categoryToUpdate = context.Find(Id);
            if (null == categoryToUpdate)
            {
                return HttpNotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View(categoryToUpdate);
            }
            else
            {
                categoryToUpdate.Category = category1.Category;
                           
                //context.Update(productToUpdate);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(String Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (null == categoryToDelete)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (null == categoryToDelete)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}