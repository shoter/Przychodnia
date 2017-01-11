using Przychodnia.Attributes;
using Przychodnia.Helpers;
using Przychodnia.Models.Pacjenci;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class PacjentController : ControllerBase
    {
        private readonly PacjentRepository pacjentRepository;
        private readonly PrzychodniaRepository przychodniaRepository;
        private readonly LekarzRepository lekarzRepository;
        private readonly ChorobaRepository chorobaRepository;

        public PacjentController(PacjentRepository pacjentRepository, PrzychodniaRepository przychodniaRepository, LekarzRepository lekarzRepository,
            ChorobaRepository chorobaRepository)
        {
            this.pacjentRepository = pacjentRepository;
            this.przychodniaRepository = przychodniaRepository;
            this.lekarzRepository = lekarzRepository;
            this.chorobaRepository = chorobaRepository;
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Index()
        {
            var pacjenci = pacjentRepository.GetForLekarz(SessionHelper.Uzytkownik.LekarzID.Value);

            var vm = new IndexViewModel(pacjenci);

            return View(vm);
        }

        [HttpGet]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Add()
        {
            var vm = new AddPacjentViewModel();

            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpGet]
        public ActionResult Przypisz()
        {
            try
            {
                var pacjenci = pacjentRepository.GetAll();

                var vm = new PrzypiszPacjentaViewModel(pacjenci);

                return View(vm);

            } catch(Exception e)
            {
                AddError(e);
                return View(new PrzypiszPacjentaViewModel());
            }

        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpPost]
        public ActionResult Przypisz(PrzypiszPacjentaViewModel vm)
        {
            try
            {
                pacjentRepository.Przypisz(vm.PacjentID, SessionHelper.Uzytkownik.LekarzID.Value);
                AddSuccess("Przypisano pacjenta!");
                var pacjenci = pacjentRepository.GetAll();
                vm = new PrzypiszPacjentaViewModel(pacjenci);

                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e);
                return View(new PrzypiszPacjentaViewModel());
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Pomiary()
        {
            try
            {
                var pomiary = pacjentRepository.GetPomiary(4);
                var vm = new PomiaryListViewModel(pomiary);

                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e);
                return View(new PomiaryListViewModel());
            }
        }

        [HttpPost]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Add(AddPacjentViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pacjentRepository.Insert(vm.Imie, vm.Nazwisko, vm.Pesel, vm.CzyMezczyzna, vm.DataUrodzenia, vm.DataZgonu, vm.Notes);
                    AddSuccess("Pacjent został dodany");
                    return View(new AddPacjentViewModel());

                }
            } catch(Exception e)
            {
                AddError(e);
            }
            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpGet]
        public ActionResult Pomiar(int? pacjentID)
        {
            var pacjenci = pacjentRepository.GetAll();
            var przydzialy = lekarzRepository.GetActivePrzydzialy(SessionHelper.Uzytkownik.LekarzID.Value);
            var vm = new PomiarViewModel(pacjentID, pacjenci, przydzialy);

            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpPost]
        public ActionResult Pomiar(PomiarViewModel vm)
        {

            try
            {
                if(ModelState.IsValid)
                {

                    pacjentRepository.AddPomiar(vm.PacjentID.Value, vm.Skurczowe.Value, vm.Rozkurczowe.Value, vm.Tetno.Value, SessionHelper.Uzytkownik.LekarzID.Value, vm.PrzychodniaID.Value, DateTime.Now);
                    AddSuccess("Pomiar został dodany!");
                    return RedirectToAction("Pomiar");
                }

                var pacjenci = pacjentRepository.GetAll();
                var przydzialy = lekarzRepository.GetActivePrzydzialy(SessionHelper.Uzytkownik.LekarzID.Value);
                vm = new PomiarViewModel(vm.PacjentID, pacjenci, przydzialy);
            } catch(Exception e)
            {
                AddError(e);
            }

            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        [HttpPost]
        public ActionResult RandomPomiary(int pacjentID, int przychodniaID)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var najstarszy = pacjentRepository.GetNajstarszyPomiar(pacjentID);
                    int skurczowe=0, rozkurczowe=0, tetno=0;
                    Random rand = new Random();
                    DateTime date = najstarszy?.Data ?? DateTime.UtcNow;
                    skurczowe = najstarszy?.Skurczowe ?? rand.Next(120, 150);
                    rozkurczowe = najstarszy?.Rozkurczowe ??  rand.Next(80, 100);
                    tetno = najstarszy?.Tetno ?? rand.Next(60, 80);

                    for (int i = 0; i < 60; ++i)
                    {
                        skurczowe += rand.Next(-10, 10);
                        rozkurczowe += rand.Next(-5, 5);
                        tetno += rand.Next(-5, 5);

                        skurczowe = Math.Min(150, skurczowe);
                        rozkurczowe = Math.Min(110, rozkurczowe);
                        tetno = Math.Min(100, tetno);

                        skurczowe = Math.Max(100, skurczowe);
                        rozkurczowe = Math.Max(60, rozkurczowe);
                        tetno = Math.Max(60, tetno);
                        pacjentRepository.AddPomiar(pacjentID, skurczowe, rozkurczowe, tetno, SessionHelper.Uzytkownik.LekarzID.Value, przychodniaID, date);
                        
                        date = date.AddDays(-1);
                    }
                    AddSuccess("Pomiary został dodany!");
                    return RedirectToAction("Pomiar");
                }

                return RedirectToAction("Pomiar", new { pacjentID = pacjentID });
            }
            catch (Exception e)
            {
                AddError(e);
                return RedirectBack();
            }

        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Info(int pacjentID)
        {
            try
            {
                var pomiary = pacjentRepository.GetPomiary(pacjentID);
                var choroby = chorobaRepository.GetStatusyForPacjent(pacjentID);
                var notatki = pacjentRepository.GetNotatki(pacjentID);
                var vm = new InfoViewModel(pomiary, choroby, notatki, pacjentID);

                return View(vm);
            } catch(Exception e)
            {
                AddError(e);
                return RedirectBack();
            }

        }

        [HttpPost]
        public JsonResult Notatka(string notatki, int pacjentID)
        {
            try
            {
                pacjentRepository.UpdateNotatki(pacjentID, notatki);
                return new JsonResult();
            } catch(Exception e)
            {
                AddError(e);
                return new JsonResult();
            }
        }
    }
}