using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Business.Concrete
{
    public class AccountManager:IAccountService
    {
        IAccountDal _accountDal;
        
        public AccountManager(IAccountDal accountDal)
        {
            _accountDal = accountDal;
            
        }

        [SecuredOperation("admin,account.admin")]
        [ValidationAspect(typeof(AccountValidator))]
        public IResult Add(Account entity)
        {
            var result = BusinessRules.Run(SetDefaultMoney(entity),SetCreateDate(entity));

            _accountDal.Add(entity);
            return new SuccessResult(Messages.AddedAccount);
        }

        [SecuredOperation("admin,account.admin")]
        public IResult Delete(Account entity)
        {
            _accountDal.Delete(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,account.admin")]
        [ValidationAspect(typeof(AccountValidator))]
        public IResult Update(Account entity)
        {
            var result = _accountDal.Get(e => e.Id == entity.Id);
            entity.Money = result.Money;
            entity.AccountCreateDate = result.AccountCreateDate;
            _accountDal.Update(entity);
            return new SuccessResult(Messages.AccountUpdated);
        }

        public IDataResult<List<Account>> GetAll()
        {
            return new SuccessDataResult<List<Account>>(_accountDal.GetAll(), Messages.AccountListed);
        }

        public IDataResult<Account> GetById(int entityId)
        {
            return new SuccessDataResult<Account>(_accountDal.Get(a => a.Id == entityId));
        }

        

        public IDataResult<int> ShowMoney(int customerId)
        {
            return new SuccessDataResult<int>(_accountDal.Get(a => a.CustomerId == customerId).Money);
        }

        public IDataResult<List<AccountDto>> GetAccountFullAttribute()
        {
            return new SuccessDataResult<List<AccountDto>>(_accountDal.GetAccountsFullAttribute(),Messages.AccountListed);
        }

        public IResult DeleteById(int id)
        {
            _accountDal.DeleteById(id);
            return new SuccessResult();
        }

        public IDataResult<List<Account>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Account>>(_accountDal.Get(a => a.CustomerId == customerId).ToString());
        }

        private IResult SetDefaultMoney(Account entity)
        {
            entity.Money = 0;
            return new SuccessResult();
        }
        private IResult SetCreateDate(Account entity)
        {
            entity.AccountCreateDate = DateTime.Now;
            return new SuccessResult();
        }


        public IResult AddMoney(MoneyAddDto moneyAddDto)
        {
            var logic = BusinessRules.Run(CheckMinMoney(moneyAddDto));
            if (logic!=null) return logic;
            var entity=_accountDal.Get(a => a.Id == moneyAddDto.Id);
            entity.Money = entity.Money+moneyAddDto.Money;
            _accountDal.Update(entity);
            return new SuccessResult(Messages.AddMoneySuccess);
        }

        public IDataResult<int> WithDrawMoney(DropMoneyDto dropMoneyDto)
        {
            var logic = BusinessRules.Run(CheckMaxMoney(dropMoneyDto));
            if (logic != null) return new ErrorDataResult<int>(logic.Message);

            var entity = _accountDal.Get(a => a.Id == dropMoneyDto.Id);
            entity.Money = entity.Money - dropMoneyDto.Money;
            _accountDal.Update(entity);
            return new SuccessDataResult<int>(dropMoneyDto.Money);
        }

        public IResult WithDrawAllMoney(int id)
        {
            var entity = _accountDal.Get(a => a.Id == id);
            entity.Money = 0;
            _accountDal.Update(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,moderatör")]
        public IDataResult<List<Account>> GetByAccountNumber(string accountNumber)
        {
            return new SuccessDataResult<List<Account>>(_accountDal.GetAll(a => a.AccountNumber == accountNumber));
        }

        public IResult CheckMaxMoney(DropMoneyDto dropMoneyDto)
        {
            var entity = _accountDal.Get(a => a.Id == dropMoneyDto.Id);
            if (entity.Money<dropMoneyDto.Money)
            {
                return new ErrorResult(Messages.InsufficientMoney);
            }
            return new SuccessResult();
        }

        private IResult CheckMinMoney(MoneyAddDto moneyAddDto)
        {
            
            if (moneyAddDto.Money<0)
            {
                return new ErrorResult(Messages.IncorrectQuantity);
            }
            return new SuccessResult();
        }
    }
}
