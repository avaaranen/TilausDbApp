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
    public class CustomersController : Controller
    {
        // GET: Customers
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
                List<Asiakkaat> model = db.Asiakkaat.ToList();
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
                Asiakkaat asiakas = db.Asiakkaat.Find(id);
                if (asiakas == null) return HttpNotFound();
                ViewBag.Postitoimipaikka = new SelectList(db.Postitoimipaikat, "Postitoimipaikka", "Postitoimipaikka");
                return View(asiakas);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Nimi, Osoite, Postinumero, Postitoimipaikka")] Asiakkaat asiakas)
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
                    db.Entry(asiakas).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(asiakas);
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

        public ActionResult Create([Bind(Include = "Nimi, Osoite, Postinumero")] Asiakkaat asiakas)
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
                    db.Asiakkaat.Add(asiakas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(asiakas);
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
                Asiakkaat asiakas = db.Asiakkaat.Find(id);
                if (asiakas == null) return HttpNotFound();
                return View(asiakas);
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
                Asiakkaat asiakas = db.Asiakkaat.Find(id);
                db.Asiakkaat.Remove(asiakas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Asiakkaat asiakas = db.Asiakkaat.Find(id);
                if (asiakas == null) return HttpNotFound();
                return View(asiakas);
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