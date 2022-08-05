using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausDbApp.Models;

namespace TilausDbApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult Map()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.LoggedStatus = "Out";
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Kayttajat LoginModel)
        {
            TilausDBEntities db = new TilausDBEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Kayttajat.SingleOrDefault(x => x.KayttajaNimi == LoginModel.KayttajaNimi && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.KayttajaNimi;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
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