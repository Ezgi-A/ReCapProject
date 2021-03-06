using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int Userid);

        IResult Add(User user);
        List<OperationClaim> GetClaims(User user);
      
       IDataResult<User> GetByMail(string email);

        IResult Delete(User user);
        IResult Update(User user);
        IResult UpdateForUser(UpdateForUserDto user);
    }
}
