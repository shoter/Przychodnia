using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Users
{
    public class AddUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatPassword { get; set; }
        public int? LekarzID { get; set; }
        public bool CreateNewOsoba { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public bool Mezczyzna { get; set; }
        public DateTime? BirthDate { get; set; }


    }
}