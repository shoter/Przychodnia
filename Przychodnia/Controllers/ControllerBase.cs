using Przychodnia.enums;
using Przychodnia.Helpers;
using Przychodnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Controllers
{
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Add message which will be displayed after next HTTP Request
        /// You can add multiple messages which will be stacked on the page
        /// </summary>
        /// <param name="message">message to display.</param>
        protected void AddMessage(PopupMessageViewModel message)
        {
            TempDataHelper.AddMessage(TempData, message);
        }

        protected void AddMessage(string content, PopupMessageType type)
        {
            AddMessage(new PopupMessageViewModel(content, type));
        }

        protected void AddError(string content)
        {
            AddMessage(content, PopupMessageType.Error);
        }


        protected void AddSuccess(string content)
        {
            AddMessage(content, PopupMessageType.Success);
        }

        protected void AddSuccess(string content, params object[] args)
        {
            AddSuccess(string.Format(content, args));
        }

        protected void AddInfo(string content)
        {
            AddMessage(content, PopupMessageType.Info);
        }

        protected void AddInfo(string content, params object[] args)
        {
            content = string.Format(content, args);
            AddInfo(content);
        }


        public HttpStatusCodeResult ServerError(string message)
        {
            return new HttpStatusCodeResult(500, message);
        }

        public RedirectToRouteResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public RedirectToRouteResult RedirectToHomeWithError(string errorMessage = "Error")
        {
            AddError(errorMessage);
            return RedirectToHome();
        }

        public ActionResult RedirectBack()
        {
            if (Request.UrlReferrer == null)
                return RedirectToHome();

            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult RedirectBackWithError(string errorMessage = "Error")
        {
            AddError(errorMessage);
            return RedirectBack();
        }

        public ActionResult RedirectBackWithInfo(string infoMessage)
        {
            AddInfo(infoMessage);
            return RedirectBack();
        }

    }
}