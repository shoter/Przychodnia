using Przychodnia.Attributes;
using Przychodnia.Models.Osoby;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class OsobaController : ControllerBase
    {
        private readonly OsobaRepository osobaRepository;

        public OsobaController(OsobaRepository osobaRepository)
        {
            this.osobaRepository = osobaRepository;
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult Index()
        {
            try
            {
                var osoby = osobaRepository.GetAll();

                return View(new OsobyListViewModel(osoby));
            }
            catch(Exception e)
            {
                AddError(e);
                return View(new OsobyListViewModel());
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpGet]
        public ActionResult Zabij(int KtoZawinilID)
        {
            try
            {
                var osoba = osobaRepository.get(KtoZawinilID);
                var vm = new UsmiercViewModel(osoba);
                return View(vm);
            } catch(Exception e)
            {
                AddError(e);
                return View(new UsmiercViewModel());
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpPost]
        public ActionResult Zabij(UsmiercViewModel vm)
        {
            try
            {
                osobaRepository.Usmierc(vm.OsobaID, vm.Date.Value);
                var osoba = osobaRepository.get(vm.OsobaID);
                AddSuccess("Pomyślnie uśmierciłeś {0} {1}", osoba.Nazwisko, osoba.Imie);
                return RedirectToAction("index", new { usmiertelniacz = true });
            } catch(Exception e)
            {
                AddError(e);
                return View(new UsmiercViewModel());
            }
        }
    }
}