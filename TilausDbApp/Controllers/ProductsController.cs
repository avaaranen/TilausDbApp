using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausDbApp.Models;

namespace TilausDbApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            TilausDBEntities db = new TilausDBEntities();
            List<Tuotteet> model = db.Tuotteet.ToList();
            db.Dispose();

            return View(model);
        }

        public ActionResult ProductCard()
        {
            TilausDBEntities db = new TilausDBEntities();
            List<Tuotteet> model = db.Tuotteet.ToList();
            db.Dispose();
            return View(model);
        }
    }
}