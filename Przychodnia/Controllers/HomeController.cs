using Przychodnia.Attributes;
using Przychodnia.Models.Home;
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
        private readonly LekarzRepository lekarzRepository;

        public HomeController(UzytkownikRepository uzytkownikRepository, LekarzRepository lekarzRepository)
        {
            this.uzytkownikRepository = uzytkownikRepository;
            this.lekarzRepository = lekarzRepository;
        }

        [PrzychodniaAuthorize]
        public ActionResult Index()
        {
            var lekarze = lekarzRepository.GetNajlepsiLekarze();
            var vm = new HomeViewModel(lekarze);

            return View(vm);
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