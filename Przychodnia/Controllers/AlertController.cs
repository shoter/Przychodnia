using Przychodnia.Attributes;
using Przychodnia.Helpers;
using Przychodnia.Models.alerty;
using PrzychodniaData.Enums;
using PrzychodniaData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class AlertController : ControllerBase
    {
        private readonly AlertRepository alertRepository;
        public AlertController(AlertRepository alertRepository)
        {
            this.alertRepository = alertRepository;
        }

        [PrzychodniaAuthorize(PrawoUzytkownikaEnum.Lekarz)]
        public ActionResult Index()
        {
            try
            {
                var alerty = alertRepository.GetAlerts(SessionHelper.Uzytkownik.LekarzID.Value);
                var vm = new AlertListViewModel(alerty);
                return View(vm);
            }
            catch (Exception e)
            {
                AddError(e);
                return View(new AlertListViewModel());
            }
        }
    }
}