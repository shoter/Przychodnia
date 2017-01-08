using Przychodnia.Attributes;
using Przychodnia.Models.Users;
using Przychodnia.Validator;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UzytkownikRepository uzytkownikRepository;
        private readonly OsobaRepository osobaRepository;
        private readonly LekarzRepository lekarzRepository;
        private readonly PrzychodniaRepository przychodniaRepository;
        private readonly KierownikRepository kierownikRepository;


        public UserController(UzytkownikRepository uzytkownikRepository, LekarzRepository lekarzRepository, OsobaRepository osobaRepository, PrzychodniaRepository przychodniaRepository,
            KierownikRepository kierownikRepository)
        {
            this.uzytkownikRepository = uzytkownikRepository;
            this.lekarzRepository = lekarzRepository;
            this.osobaRepository = osobaRepository;
            this.przychodniaRepository = przychodniaRepository;
            this.kierownikRepository = kierownikRepository;
        }

        [HttpGet]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult AddUser()
        {
            var osoby = osobaRepository.GetAll();

            var vm = new AddUserViewModel();
            vm.SetLekarze(osoby);

            return View(vm);
        }

        [HttpPost]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult AddUser(AddUserViewModel vm)
        {
            AddUserViewModelValidator validator = new AddUserViewModelValidator(ModelState);
            try
            {
                var osoby = osobaRepository.GetAll();
                vm.SetLekarze(osoby);

                if (validator.Validate(vm))
                {
                    if (vm.CreateNewOsoba == false)
                    {
                        uzytkownikRepository.CreateAccount(vm.Username, vm.Password, vm.OsobaID.Value);
                        AddSuccess("Stworzono konto {0}", vm.Username);
                    }
                    else
                    {
                        uzytkownikRepository.CreateAccount(vm.Username, vm.Password, vm.Name, vm.Surname, vm.Pesel, vm.BirthDate.Value, vm.Mezczyzna);
                        AddSuccess("Stworzono konto {0}", vm.Username);
                    }
                }
                
            } catch(Exception e)
            {
                AddError(e.Message);
            }
            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult Index(UserListViewModel vm)
        {
            var users = uzytkownikRepository.GetAll(vm.AktualnePrawoID);
            vm = new UserListViewModel(users, vm.AktualnePrawoID);
            return View(vm);
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpGet]
        public ActionResult ChangeUser(int userID)
        {
            try
            {
                var user = uzytkownikRepository.Get(userID);
                var przychodnie = przychodniaRepository.GetAll();
                var vm = new ChangeUserViewModel(user, przychodnie);
                if (user.LekarzID.HasValue)
                {
                    var zatrudnienia = lekarzRepository.GetPrzydzialy(user.LekarzID.Value);
                    vm.zaladuj(zatrudnienia);
                }


                return View(vm);
            } catch(Exception e)
            {
                AddError(e.Message);
                return NullView();
            }
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        [HttpPost]
        public ActionResult ChangeUser(ChangeUserViewModel vm)
        {
            try
            {
                vm.MakeAbsentValuesAsFalse();

                string settingsString = "";

                foreach (var prawo in vm.Prawa)
                {
                    settingsString += string.Format("{0},{1};", (int)prawo.Prawo, prawo.Value ? 1 : 0);
                }

                settingsString = settingsString.Substring(0, settingsString.Length - 1);

                uzytkownikRepository.UpdatePrawa(vm.UserID, settingsString);

                settingsString = "";
                foreach (var kierownik in vm.Kierownictwo)
                {
                    settingsString += string.Format("{0},{1};", (int)kierownik.PrzychodniaID, kierownik.Value ? 1 : 0);
                }
                if (string.IsNullOrWhiteSpace(settingsString) == false)
                    kierownikRepository.UpdateKierownik(vm.LekarzID.Value, settingsString);


                var user = uzytkownikRepository.Get(vm.UserID);

                var przychodnie = przychodniaRepository.GetAll();
                vm = new ChangeUserViewModel(user, przychodnie);
                if (user.LekarzID.HasValue)
                {
                    var zatrudnienia = lekarzRepository.GetPrzydzialy(user.LekarzID.Value);
                    vm.zaladuj(zatrudnienia);
                }

                return View(vm);
            }
            catch (Exception e)
            {
                AddError(e.Message);
                return NullView();
            }
        }
    }
}