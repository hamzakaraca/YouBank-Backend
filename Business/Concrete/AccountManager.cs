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
            return new SuccessResult();
        }

        public IDataResult<List<Account>> GetAll()
        {
            return new SuccessDataResult<List<Account>>(_accountDal.GetAll(), Messages.AccountListed);
        }

        public IDataResult<Account> GetById(int entityId)
        {
            return new SuccessDataResult<Account>(_accountDal.Get(a => a.Id == entityId));
        }

        

        public IDataResult<string> ShowMoney(int customerId)
        {
            return new SuccessDataResult<string>(_accountDal.Get(a => a.CustomerId == customerId).Money);
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
            entity.Money = "0";
            return new SuccessResult();
        }
        private IResult SetCreateDate(Account entity)
        {
            entity.AccountCreateDate = DateTime.Now;
            return new SuccessResult();
        }
    }
}
