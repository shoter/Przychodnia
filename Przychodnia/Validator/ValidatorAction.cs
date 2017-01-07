using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Validator
{
    public enum ValidatorAction
    {
        Create = 1,
        Edit = 2,
        Delete = 3,
        Login = 4,
        Register = 5,
        Undefined
    }
}