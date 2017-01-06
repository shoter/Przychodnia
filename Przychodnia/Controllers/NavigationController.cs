using Przychodnia.Models.Navigation;
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
        }
    }
}