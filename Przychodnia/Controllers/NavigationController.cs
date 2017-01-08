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
                }; manager.Add(przychodnie);

                foreach (var kierownik in SessionHelper.Uzytkownik.Kierownicy)
                {
                    var przychodnia = new NavigationSectionViewModel()
                    {
                        Name = kierownik.PrzychodniaNazwa,
                        Icon = "fa fa-wrench",
                        Url = Url.Action("Manage", "Management", new { przychodniaID = kierownik.PrzychodniaID })
                    };przychodnie.Add(przychodnia);
                    
                }

                
            }

            if(SessionHelper.Uzytkownik.Is(PrawoUzytkownikaEnum.Lekarz))
            {
                var manager = new NavigationSectionViewModel()
                {
                    Name = "Lekarz",
                    Icon = "fa fa-stethoscope"
                }; vm.Add(manager);

                var pacjenci = new NavigationSectionViewModel()
                {
                    Name = "Pacjenci",
                    Icon = "fa fa-users"
                }; manager.Add(pacjenci);

                var przegladaj = new NavigationSectionViewModel()
                {
                    Name = "Lista pacjentów",
                    Icon = "fa fa-wheelchair",
                    Url = Url.Action("Index", "Pacjent")
                }; pacjenci.Add(przegladaj);

                var dodaj = new NavigationSectionViewModel()
                {
                    Name = "Dodaj pacjenta",
                    Url = Url.Action("Add", "Pacjent")
                }; pacjenci.Add(dodaj);

                var choroby = new NavigationSectionViewModel()
                {
                    Name = "Choroby",
                    Icon = "fa fa-medkit"
                }; manager.Add(choroby);

                var dziennik = new NavigationSectionViewModel()
                {
                    Name = "Dzienniki chorób",
                    Icon = "fa fa-address-book",
                    Url = Url.Action("Index", "Choroba")
                }; choroby.Add(dziennik);

                var dodajWpis = new NavigationSectionViewModel()
                {
                    Name = "Dodaj wpis",
                    Url = Url.Action("Add", "Choroba")
                }; choroby.Add(dodajWpis);

                var pomiary = new NavigationSectionViewModel()
                {
                    Name = "Pomiary",
                    Icon = "fa fa-heartbeat"
                };manager.Add(pomiary);

                var pomiaryAdd = new NavigationSectionViewModel()
                {
                    Name = "Dodaj nowy pomiar",
                    Url = Url.Action("Dodaj", "Pomiar")
                }; pomiary.Add(pomiaryAdd);

                var pomiaryList = new NavigationSectionViewModel()
                {
                    Name = "Lista pomiarów",
                    Url = Url.Action("Index", "Pomiar")
                }; pomiary.Add(pomiaryList);

                
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