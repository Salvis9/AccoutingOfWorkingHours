using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Validations
{
    public interface IBaseValidator<T> where T : class
    {
        BaseResult ValidateOnNull(T model);
    }
}
