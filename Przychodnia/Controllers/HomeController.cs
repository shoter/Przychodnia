using Przychodnia.Attributes;
using PrzychodniaData;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class HomeController : Controller
    {
        private readonly UzytkownikRepository uzytkownikRepository;

        public HomeController(UzytkownikRepository uzytkownikRepository)
        {
            this.uzytkownikRepository = uzytkownikRepository;
        }

        [PrzychodniaAuthorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}