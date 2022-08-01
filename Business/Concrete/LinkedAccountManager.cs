using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LinkedAccountManager:ILinkedAccountService
    {
        ILinkedAccountDal _linkedAccountDal;

        public LinkedAccountManager(ILinkedAccountDal linkedAccountDal)
        {
            _linkedAccountDal = linkedAccountDal;
        }

        public IResult Add(LinkedAccount entity)
        {
            _linkedAccountDal.Add(entity);
            return new SuccessResult("hesaplar bağlandı");
        }

        public IResult Delete(LinkedAccount entity)
        {
            _linkedAccountDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult Update(LinkedAccount entity)
        {
            _linkedAccountDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<LinkedAccount>> GetAll()
        {
            return new SuccessDataResult<List<LinkedAccount>>(_linkedAccountDal.GetAll());
        }

        public IDataResult<LinkedAccount> GetById(int entityId)
        {
            return new SuccessDataResult<LinkedAccount>(_linkedAccountDal.Get(la => la.Id == entityId));
        }
    }
}
