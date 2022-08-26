using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Business;
using Core.Utilities.IntegratedValidations.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICostumerDal _costomerDal;
        ICheckPersonService _checkPersonService;
        IUserService _userService;
        public CustomerManager(ICostumerDal costomerDal, ICheckPersonService checkPersonService, IUserService userService)
        {
            _costomerDal = costomerDal;
            _checkPersonService = checkPersonService;
            _userService = userService;
        }

        [SecuredOperation("admin,customer.admin")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            

            var userResult = _userService.GetById(customer.UserId);
            if (!userResult.Success) return new ErrorResult(userResult.Message);

            if (!_checkPersonService.VerifyNationalityNumber(new CheckPersonDto { NationalityNumber = Convert.ToInt64(customer.NationalityNumber), FirstName = userResult.Data.FirstName, LastName = userResult.Data.LastName, YearOfBirth = customer.DateOfBirth.Year }))
                return new ErrorResult(Messages.PersonNotFound);
            var logicsResult = BusinessRules.Run(CheckExistCustomer(customer.NationalityNumber));
            if (logicsResult != null) return logicsResult;
            _costomerDal.Add(customer);
            return new SuccessResult(Messages.AddedCostumer);
        }

        [SecuredOperation("admin,customer.admin")]
        public IResult Delete(Customer entity)
        {
            _costomerDal.Delete(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,customer.admin")]
        public IResult Update(Customer entity)
        {
            _costomerDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_costomerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int entityId)
        {
            return new SuccessDataResult<Customer>(_costomerDal.Get(c => c.Id == entityId));
        }

        private IResult CheckExistCustomer(string nationalityNumber)
        {
            var result = _costomerDal.Get(c => c.NationalityNumber == nationalityNumber);
            if (result != null) return new ErrorResult(Messages.NationalityNumberAlreadyExist);

            return new SuccessResult();
        }
    }
}
