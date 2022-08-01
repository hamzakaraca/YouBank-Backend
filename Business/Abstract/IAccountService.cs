using System;
using System.Collections.Generic;
using System.Text;
using Core.Business;
using Core.Utilities.Results;
using Entities;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAccountService:IEntityServiceBase<Account>
    {
        IDataResult<List<Account>> GetByCustomerId(int customerId);
        IDataResult<string> ShowMoney(int customerId);
        IDataResult<List<AccountDto>> GetAccountFullAttribute();
        IResult DeleteById(int id);
    }
}
