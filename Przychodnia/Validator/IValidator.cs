using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Validator
{
    public interface IValidator<TEntity>
        where TEntity : class
    {

        List<ValidationError> ValidationErrors { get; set; }
        bool IsValid { get; set; }

        bool Validate(TEntity Entity, ValidatorAction action = ValidatorAction.Undefined);
    }
}
