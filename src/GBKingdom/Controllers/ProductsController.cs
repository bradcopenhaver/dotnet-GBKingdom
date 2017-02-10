using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GBKingdom.Models;
using Microsoft.EntityFrameworkCore;

namespace GBKingdom.Controllers
{
    public class ProductsController : Controller
    {
        private GBKingdomContext db = new GBKingdomContext();
        public IActionResult Index()
        {
            return View(db.Products
                .Include(prod => prod.Reviews)
                .ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            Product thisProduct = db.Products
                .Include(prod => prod.Reviews)
                .FirstOrDefault(products => products.ProductId == id);
            return View(thisProduct);
        }
        public IActionResult Edit(int id)
        {
            Product thisProduct = db.Products.FirstOrDefault(prod => prod.ProductId == id);
            return View(thisProduct);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = product.ProductId });
        }
        public IActionResult Delete(int id)
        {
            Product thisProduct = db.Products
                .Include(prod => prod.Reviews)
                .FirstOrDefault(prod => prod.ProductId == id);
            return View(thisProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product thisProduct = db.Products.FirstOrDefault(prod => prod.ProductId == id);
            db.Products.Remove(thisProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
