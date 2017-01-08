﻿using Data.Objects;
using Przychodnia.Attributes;
using Przychodnia.Helpers;
using Przychodnia.Models.Przychodnie;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class ManagementController : ControllerBase
    {
        private readonly PrzychodniaRepository przychodniaRepository;
        private readonly LekarzRepository lekarzRepository;
        public ManagementController(PrzychodniaRepository przychodniaRepository, LekarzRepository lekarzRepository)
        {
            this.przychodniaRepository = przychodniaRepository;
            this.lekarzRepository = lekarzRepository;
        }
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Kierownik)]
        [HttpGet]
        public ActionResult AddPrzydzial(int przychodniaID)
        {
            try
            {
                if (isAble(przychodniaID) == false)
                    return NotAble();

                var lekarze = lekarzRepository.GetAll();
                var przychodnia = przychodniaRepository.Get(przychodniaID);

                var vm = new AddPrzydzialViewModel(lekarze, przychodnia);

                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e.Message);
                return View(new AddPrzydzialViewModel(new List<PrzychodniaData.Objects.Lekarz>(), new PrzychodniaData.Objects.Przychodnia()));
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Kierownik)]
        [HttpPost]
        public ActionResult AddPrzydzial(AddPrzydzialViewModel vm)
        {
            try
            {
                lekarzRepository.AddPrzydzial(vm.LekarzID, vm.PrzychodniaID, DateTime.Now, null);
                AddSuccess("Dodano przydział!");
                return RedirectToAction("Manage", new { przychodniaID = vm.PrzychodniaID });
            }
            catch (Exception e)
            {
                AddError(e.Message);
                return View(vm);
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult Index()
        {
            try
            {
                var przychodnie = przychodniaRepository.GetAll();
                var vm = new ListPrzychodnieViewModel(przychodnie);
                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e.Message);
                return View(new ListPrzychodnieViewModel(new List<PrzychodniaData.Objects.Przychodnia>()));
            }
            
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Kierownik)]
        public ActionResult Manage(int przychodniaID)
        {
            try
            {
            if (isAble(przychodniaID) == false)
                return NotAble();

                var lekarze = lekarzRepository.GetMany(przychodniaID);
            var przychodnia = przychodniaRepository.Get(przychodniaID);

            var vm = new ManagePrzychodniaViewModel(przychodnia, lekarze);

            return View(vm);
            }
            catch(Exception e)
            {
                AddError(e.Message);
                return View(new ManagePrzychodniaViewModel(new PrzychodniaData.Objects.Przychodnia(), new List<PrzychodniaData.Objects.Lekarz>()));
            }
}

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpPost]
        public ActionResult Add(AddPrzychodniaViewModel vm)
        {
            try
            {
                przychodniaRepository.Add(vm.Nazwa);
                AddSuccess("Przychodnia {0} została dodana!", vm.Nazwa);

            } catch(Exception e)
            {
                AddError(e.Message);
                return View(vm);
            }
            return View();
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Kierownik)]
        public ActionResult Fire(int przychodniaID, int lekarzID)
        {
            try
            {
                if (isAble(przychodniaID) == false)
                    return NotAble();

                lekarzRepository.Fire(przychodniaID, lekarzID);
                AddSuccess("Pracownik został zwolniony z przydziału.");
                return RedirectBack();
            }
            catch (Exception e)
            {
                AddError(e);
                return RedirectBack();
            }
        }

        private bool isAble(int przychodniaID)
        {
            if (SessionHelper.Uzytkownik.Is(PrawoUzytkownikaEnum.Administrator))
                return true;

            foreach(var kierownik in SessionHelper.Uzytkownik.Kierownicy)
            {
                if (kierownik.PrzychodniaID == przychodniaID && kierownik.KoniecPrzydzialu.HasValue == false)
                    return true;
            }

            return false;
        }

        public ActionResult NotAble()
        {
            return RedirectBackWithError("Nie jesteś kierownikiem tej placówki aby nią zarządzać!");
        }



    }
}