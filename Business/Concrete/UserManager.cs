using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [SecuredOperation("User.admin,admin")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        [SecuredOperation("User.admin,admin")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        public IResult ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            var rulesResult = BusinessRules.Run(CheckIfNewPasswordsMatch(updatePasswordDto.NewPassword, updatePasswordDto.NewPasswordAgain),CheckSamePasswords(updatePasswordDto.Password, updatePasswordDto.NewPassword));
            if (rulesResult != null) return rulesResult;

            var userResult = _userDal.Get(u => u.Id == updatePasswordDto.Id);

            var verifyResult = HashingHelper.VerifyPasswordHash(updatePasswordDto.Password, userResult.PasswordHash, userResult.PasswordSalt);
            if (!verifyResult) return new ErrorResult(Messages.PasswordError);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updatePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;

            _userDal.Update(userResult);
            return new SuccessResult(Messages.PasswordUpdated);
        }

        private IResult CheckIfNewPasswordsMatch(string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain) return new ErrorResult(Messages.NewPasswordsMatchError);
            return new SuccessResult();
        }

        private IResult CheckSamePasswords(string newPassword, string password)
        {
            if (newPassword == password) return new ErrorResult(Messages.PasswordsSame);
            return new SuccessResult();
        }
    }
}
