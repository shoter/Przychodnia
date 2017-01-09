using PrzychodniaData.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Account
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<LoginInfo> Loginy { get; set; } = new List<LoginInfo>();

        public LoginViewModel() { }
        public LoginViewModel(List<LoginInfo> loginy)
        {
            Loginy = loginy;
        }
    }
}