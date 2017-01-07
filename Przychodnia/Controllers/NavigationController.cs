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

            if(SessionHelper.Uzytkownik.Is(PrawoUzytkownikaEnum.Administrator))
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

                
                userManagement.Add(addUser);
                userManagement.Add(Users);
            }

            vm.Add(home);
        }

        public PartialViewResult DisplayTop()
        {
            var user = SessionHelper.Uzytkownik;

            var vm = new TopViewModel(user);

            return PartialView(vm);
        }
    }
}