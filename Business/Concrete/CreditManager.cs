using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CreditManager:ICreditService
    {
        private ICreditDal _creditDal;

        public CreditManager(ICreditDal creditDal)
        {
            _creditDal = creditDal;
        }

        [SecuredOperation("admin,credit.admin")]
        public IResult Add(Credit entity)
        {
            _creditDal.Add(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,credit.admin")]
        public IResult Delete(Credit entity)
        {
            _creditDal.Delete(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,credit.admin")]
        public IResult Update(Credit entity)
        {
            _creditDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Credit>> GetAll()
        {
            return new SuccessDataResult<List<Credit>>(_creditDal.GetAll());
        }

        public IDataResult<Credit> GetById(int entityId)
        {
            return new SuccessDataResult<Credit>(_creditDal.Get(c => c.CustomerId == entityId));
        }
    }
}
