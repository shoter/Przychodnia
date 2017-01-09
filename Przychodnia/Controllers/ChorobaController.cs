using Przychodnia.Attributes;
using Przychodnia.Helpers;
using Przychodnia.Models.Choroby;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class ChorobaController : ControllerBase
    {
        private readonly ChorobaRepository chorobaRepository;
        private readonly PrzychodniaRepository przychodniaRepository;
        private readonly LekarzRepository lekarzRepository;
        private readonly PacjentRepository pacjentRepository;

        public ChorobaController(ChorobaRepository chorobaRepository, PrzychodniaRepository przychodniaRepository, LekarzRepository lekarzRepository, PacjentRepository pacjentRepository)
        {
            this.chorobaRepository = chorobaRepository;
            this.przychodniaRepository = przychodniaRepository;
            this.lekarzRepository = lekarzRepository;
            this.pacjentRepository = pacjentRepository;
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpGet]
        public ActionResult Add(int pacjentID)
        {
            try
            {
                var przydzialy = lekarzRepository.GetActivePrzydzialy(SessionHelper.Uzytkownik.LekarzID.Value);
                var dzienniki = chorobaRepository.GetDziennikiForLekarz(SessionHelper.Uzytkownik.LekarzID.Value, pacjentID);
                var choroby = chorobaRepository.GetChoroby();
                var pacjenci = pacjentRepository.GetForLekarz(SessionHelper.Uzytkownik.LekarzID.Value);
                var vm = new AddChorobaViewModel(dzienniki, przydzialy, choroby,  pacjentID, pacjenci);

                return View(vm);
            }
            catch (Exception e)
            {
                AddError(e);
                return View(new AddChorobaViewModel());
            }

        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpPost]
        public ActionResult Add(AddChorobaViewModel vm)
        {

            try
            {
                if(vm.NowyDziennik == false && vm.DziennikID.HasValue == false)
                {
                    AddError("Musisz wybrać dziennik jeśli nie tworzysz nowego!");
                }
                else
                {
                    int dziennikID = 0;
                    if(vm.NowyDziennik == true)
                    {
                        dziennikID = chorobaRepository.DodajDziennik(vm.PacjentID, vm.ChorobaID, vm.NazwaDziennika);
                        AddSuccess("Dodano dziennik " + vm.NazwaDziennika);
                    }
                    else
                    {
                        dziennikID = vm.DziennikID.Value;
                    }

                    chorobaRepository.DodajWpis(dziennikID, DateTime.UtcNow, vm.Notatka, SessionHelper.Uzytkownik.LekarzID.Value, vm.PrzychodnieID);
                    AddSuccess("Dodano wpis o chorobie!");
                }

                var przydzialy = lekarzRepository.GetActivePrzydzialy(SessionHelper.Uzytkownik.LekarzID.Value);
                var dzienniki = chorobaRepository.GetDziennikiForLekarz(SessionHelper.Uzytkownik.LekarzID.Value, vm.PacjentID);
                var choroby = chorobaRepository.GetChoroby();
                var pacjenci = pacjentRepository.GetForLekarz(SessionHelper.Uzytkownik.LekarzID.Value);
                vm.Init(dzienniki, przydzialy, choroby, pacjenci);
            } catch(Exception e)
            {
                AddError(e);
            }
            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Dziennik(int dziennikID)
        {
            try
            {
                var dziennik = chorobaRepository.GetDziennik(dziennikID);
                var statusy = chorobaRepository.GetStatusyForDziennik(dziennikID);
                var przydzialy = lekarzRepository.GetActivePrzydzialy(SessionHelper.Uzytkownik.LekarzID.Value);
                var vm = new DziennikViewModel(dziennik, statusy, przydzialy);

                return View(vm);
            } 
            catch (Exception e)
            {
                AddError(e);
                return View(new DziennikViewModel());
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Index()
        {
            try
            {
                var dzienniki = chorobaRepository.GetDziennikiForLekarz(SessionHelper.Uzytkownik.LekarzID.Value);

                var vm = new DziennikiListIndexViewModel(dzienniki);

                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e);
                return View();
            }
        }
    }
}