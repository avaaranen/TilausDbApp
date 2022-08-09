using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDbApp.Models;

namespace TilausDbApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        TilausDBEntities db = new TilausDBEntities();

        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                List<Tuotteet> model = db.Tuotteet.ToList();
                return View(model);
            }
        }

        public ActionResult ProductCard()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                List<Tuotteet> model = db.Tuotteet.ToList();
                return View(model);
            }
        }

        
        public ActionResult Edit(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tuotteet tuote = db.Tuotteet.Find(id);
                if (tuote == null) return HttpNotFound();
                return View(tuote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "TuoteID, Nimi, Ahinta, Kuvalinkki")] Tuotteet tuote)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (ModelState.IsValid)
                {
                    db.Entry(tuote).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ProductCard");
                }
                return View(tuote);
            }
        }

        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Nimi, Ahinta, Kuvalinkki")] Tuotteet tuote)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (ModelState.IsValid)
                {
                    db.Tuotteet.Add(tuote);
                    db.SaveChanges();
                    return RedirectToAction("ProductCard");
                }
                return View(tuote);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tuotteet tuote = db.Tuotteet.Find(id);
                if (tuote == null) return HttpNotFound();
                return View(tuote);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                Tuotteet tuote = db.Tuotteet.Find(id);
                db.Tuotteet.Remove(tuote);
                db.SaveChanges();
                return RedirectToAction("ProductCard");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }
}