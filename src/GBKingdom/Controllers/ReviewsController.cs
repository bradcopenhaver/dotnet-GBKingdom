using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GBKingdom.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GBKingdom.Controllers
{
    public class ReviewsController : Controller
    {
        private GBKingdomContext db = new GBKingdomContext();
        public IActionResult Create(int id)
        {
            Product thisProduct = db.Products.FirstOrDefault(prod => prod.ProductId == id);
            ViewBag.product = thisProduct;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", "Products", new { id = review.ProductId });
        }
        public IActionResult Edit(int id)
        {
            Review thisReview = db.Reviews.FirstOrDefault(rev => rev.ReviewId == id);
            return View(thisReview);
        }
        [HttpPost]
        public IActionResult Edit(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Products", new { id = review.ProductId });
        }
        public IActionResult Delete(int id)
        {
            Review thisReview = db.Reviews
                .Include(rev => rev.Product)
                .FirstOrDefault(rev => rev.ReviewId == id);
            ViewBag.productName = thisReview.Product.Name;
            return View(thisReview);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Review thisReview = db.Reviews.FirstOrDefault(rev => rev.ReviewId == id);
            db.Reviews.Remove(thisReview);
            db.SaveChanges();
            return RedirectToAction("Details", "Products", new { id = thisReview.ProductId });
        }
    }
}
