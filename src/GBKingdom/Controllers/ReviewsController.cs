using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GBKingdom.Models;

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
    }
}
