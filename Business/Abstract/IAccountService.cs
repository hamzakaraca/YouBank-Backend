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
        IDataResult<int> ShowMoney(int customerId);
        IDataResult<List<AccountDto>> GetAccountFullAttribute();
        IResult DeleteById(int id);
        IResult AddMoney(MoneyAddDto moneyAddDto);
        IDataResult<int> WithDrawMoney(DropMoneyDto dropMoneyDto);
        IResult WithDrawAllMoney(int id);
        IDataResult<List<Account>> GetByAccountNumber(string accountNumber);

        //rules
        IResult CheckMaxMoney(DropMoneyDto dropMoneyDto);
    }
}
