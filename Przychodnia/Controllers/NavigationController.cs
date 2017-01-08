using Data.Objects;
using Przychodnia.Helpers;
using Przychodnia.Models.Navigation;
using PrzychodniaData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class NavigationController : Controller
    {
        public PartialViewResult RenderNavigation()
        {
            var vm = new NavigationSectionViewModel();

            prepareNavigation(vm);

            return PartialView(vm);
        }

        private void prepareNavigation(NavigationSectionViewModel vm)
        {
            var home = new NavigationSectionViewModel()
            {
                Name = "Strona Główna",
                Icon = "fa fa-home",
                Url = Url.Action("Index", "Home")
            };
            vm.Add(home);

            if (SessionHelper.Uzytkownik.Is(PrawoUzytkownikaEnum.Administrator))
            {
                var admin = new NavigationSectionViewModel()
                {
                    Name = "Administrator",
                    Icon = "fa fa-wrench"
                };
                vm.Add(admin);

                var userManagement = new NavigationSectionViewModel()
                {
                    Name = "Zarządzanie użytkownikami",
                    Icon = "fa fa-user"
                };

                admin.Add(userManagement);

                var addUser = new NavigationSectionViewModel()
                {
                    Name = "Dodaj użytkownika",
                    Icon = "fa fa-user-plus",
                    Url = Url.Action("AddUser", "User")
                };

                var Users = new NavigationSectionViewModel()
                {
                    Name = "Użytkownicy",
                    Icon = "fa fa-users",
                    Url = Url.Action("Index", "User")
                };


                var medicalManagement = new NavigationSectionViewModel()
                {
                    Name = "Zarządzanie przychodniami",
                    Icon = "fa fa-medkit"
                };
                admin.Add(medicalManagement);

                var addMedical = new NavigationSectionViewModel()
                {
                    Name = "Dodaj przychodnie",
                    Icon = "fa fa-plus-square",
                    Url = Url.Action("Add", "Management")
                };

                var medicals = new NavigationSectionViewModel()
                {
                    Name = "Przychodnie",
                    Icon = "fa fa-medkit",
                    Url = Url.Action("Index", "Management")
                };

                medicalManagement.Add(addMedical);
                medicalManagement.Add(medicals);

                userManagement.Add(addUser);
                userManagement.Add(Users);
            }

            if(SessionHelper.Uzytkownik.Is(PrawoUzytkownikaEnum.Kierownik))
            {
                var manager = new NavigationSectionViewModel()
                {
                    Name = "Kierownik",
                    Icon = "fa fa-suitcase"
                }; vm.Add(manager);
                var przychodnie = new NavigationSectionViewModel()
                {
                    Name = "Przychodnie",
                    Icon = "fa fa-medkit"
                };

                manager.Add(przychodnie);
            }

            
        }

        public PartialViewResult DisplayTop()
        {
            var user = SessionHelper.Uzytkownik;

            var vm = new TopViewModel(user);

            return PartialView(vm);
        }
    }
}