using Przychodnia.Attributes;
using Przychodnia.Helpers;
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
            try
            {
                AddMessage("Zaloguj się", enums.PopupMessageType.Warning);
                var loginy = uzytkownikRepository.GetLoginInfo();
                var vm = new LoginViewModel(loginy);
                return View(vm);
            }
            catch(Exception e)
            {
                AddError(e);
                return View(new LoginViewModel());
            }
            
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            try
            {
                var user = userService.Login(vm.UserName, vm.Password);

                if (user != null)
                {
                    AddSuccess("Zalogowano jako {0}.", user.nazwaUzytkownika);
                    return RedirectToHome();
                }
                else
                {
                    AddError("Login użytkownika lub/i hasło są nieprawidłowe!");
                    var loginy = uzytkownikRepository.GetLoginInfo();
                    vm = new LoginViewModel(loginy);
                }

            } catch(Exception e)
            {
                AddError(e.Message);
                
            }

            return View(vm);
        }

        [HttpGet]
        [PrzychodniaAuthorize]
        public ActionResult Logout()
        {
            try
            {
                sesjaRepository.Remove(SessionHelper.Uzytkownik.ID);
                SessionHelper.Sesja = null;
            } catch(Exception e)
            {
                AddError(e.Message);
            }
            return RedirectToAction("Login");
        }
    }
}