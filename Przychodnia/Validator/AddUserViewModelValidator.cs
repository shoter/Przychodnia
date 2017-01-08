using Przychodnia.Models.Account;
using Przychodnia.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Przychodnia.Validator
{
    public class AddUserViewModelValidator : Validator<AddUserViewModel>
    {
        public AddUserViewModelValidator()
        {
        }

        public AddUserViewModelValidator(ModelStateDictionary ModelState) : base(ModelState)
        {

        }

        public override bool Validate(AddUserViewModel model, ValidatorAction action = ValidatorAction.Undefined)
        {
            if(model.CreateNewOsoba)
            {
                if(validatePesel(model.Pesel) == false)
                {
                 //   AddError("Zły numer PESEL!", () => model.Pesel);
                }
                if(model.BirthDate.HasValue == false)
                {
                    AddError("Musisz wybrać date urodzenia!", () => model.BirthDate);
                }
            }
            else
            {
                if (model.OsobaID.HasValue == false)
                    AddError("Musisz wybrać osobe!");
            }

            return IsValid;
        }

        public static bool validatePesel(string szPesel)
        {
            byte[] tab = new byte[10] { 9, 7, 3, 1, 9, 7, 3, 1, 9, 7 };
            byte[] tablicz = new byte[] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
            bool bResult = false;
            int suma = 0;
            int sumcontrol = 0;

            szPesel = szPesel.Trim();

            if (szPesel.Length == 11)
            {
                foreach (char l in szPesel)
                {
                    byte b = Convert.ToByte(l);
                    if (Array.IndexOf(tablicz, Convert.ToByte(l)) == -1) return false;
                }

                sumcontrol = Convert.ToInt32(szPesel[10].ToString());

                for (int i = 0; i < 10; i++)
                {
                    suma += tab[i] * Convert.ToInt32(szPesel[i].ToString());
                }

                bResult = ((suma % 10) == sumcontrol);

                if (bResult)
                {
                    int rok = 0;
                    int mies = 0;
                    int dzien = Convert.ToInt32(szPesel[4].ToString()) * 10 + Convert.ToInt32(szPesel[5].ToString());

                    if (szPesel[2] == '0' || szPesel[2] == '1')
                    {
                        rok = 1900;
                        mies = Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString());
                    }
                    else if (szPesel[2] == '2' || szPesel[2] == '3')
                    {
                        rok = 2000;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 20);
                    }
                    else if (szPesel[2] == '4' || szPesel[2] == '5')
                    {
                        rok = 2100;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 40);
                    }
                    else if (szPesel[2] == '6' || szPesel[2] == '7')
                    {
                        rok = 2200;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 60);
                    }
                    else if (szPesel[2] == '8' || szPesel[2] == '9')
                    {
                        rok = 1800;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 80);
                    }
                    rok += Convert.ToInt32(szPesel[0].ToString()) * 10 + Convert.ToInt32(szPesel[1].ToString());
                    String szDate = rok.ToString() + "-" + (mies < 10 ? "0" + mies.ToString() : mies.ToString()) + "-" + (dzien < 10 ? "0" + dzien.ToString() : dzien.ToString());
                    DateTime dt;
                    bResult = DateTime.TryParse(szDate, out dt);
                }
            }
            else return false;

            return bResult;
        }

    }
}