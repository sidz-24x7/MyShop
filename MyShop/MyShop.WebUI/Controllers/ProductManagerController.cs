﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.core.Contracts;
using MyShop.core.Models;
using MyShop.core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> prodcutCategoryContext)
        {

            context = productContext;
            productCategories = prodcutCategoryContext;
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
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
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
        public ActionResult Edit(Product product, String Id, HttpPostedFileBase file)
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
                if (file != null)
                {
                    productToUpdate.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToUpdate.Image);
                }

                productToUpdate.Category = product.Category;
                productToUpdate.Description = product.Description;
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;

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
                context.Delete(productToDelete.Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}