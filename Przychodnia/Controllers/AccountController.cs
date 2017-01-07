using Przychodnia.Models.Account;
using Przychodnia.Services;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UzytkownikRepository uzytkownikRepository;
        private readonly SesjaRepository sesjaRepository;
        private readonly UserService userService;

        public AccountController(UzytkownikRepository uzytkownikRepository, SesjaRepository sesjaRepository, UserService userService)
        {
            this.uzytkownikRepository = uzytkownikRepository;
            this.sesjaRepository = sesjaRepository;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            AddMessage("Zaloguj się", enums.PopupMessageType.Warning);
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            var user = userService.Login(vm.UserName, vm.Password);

            if(user != null)
            {
                AddSuccess("Zalogowano jako {0}.", user.nazwaUzytkownika);
                return RedirectToHome();
            }
            else
            {
                AddError("Login użytkownika lub/i hasło są nieprawidłowe!");
            }

            return View(vm);
        }
    }
}