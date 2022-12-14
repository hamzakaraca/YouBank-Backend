using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int id);
        IResult Delete(User user);
        IResult Update(User user);
        IDataResult<List<User>> GetAll();

        IResult ChangePassword(UpdatePasswordDto updatePasswordDto);
    }
}