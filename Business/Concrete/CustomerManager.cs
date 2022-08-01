using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        ICostumerDal _costomerDal;

        public CustomerManager(ICostumerDal costomerDal)
        {
            _costomerDal = costomerDal;
        }

        [SecuredOperation("admin,customer.admin")]
        public IResult Add(Customer entity)
        {
            _costomerDal.Add(entity);
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

    }
}
