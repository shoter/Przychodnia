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

        public UserController(UzytkownikRepository uzytkownikRepository)
        {
            this.uzytkownikRepository = uzytkownikRepository;
        }

        [HttpGet]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Administrator)]
        public ActionResult AddUser(AddUserViewModel vm)
        {
            AddUserViewModelValidator validator = new AddUserViewModelValidator(ModelState);
            try
            {
                if (validator.Validate(vm))
                {
                    if (vm.CreateNewOsoba == false)
                    {
                        uzytkownikRepository.CreateAccount(vm.Username, vm.Password, vm.LekarzID.Value);
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
    }
}