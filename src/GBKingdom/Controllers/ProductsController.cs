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
    }
}
