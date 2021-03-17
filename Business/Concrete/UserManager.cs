using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _ıuserDal;
        public UserManager(IUserDal ıuserdal)
        {
            _ıuserDal = ıuserdal;
        }
        public IResult Add(User user)
        {
            _ıuserDal.Add(user);
            return new SuccessResult(Messages.UserAdding);
        }

        public IResult Delete(User user)
        {
            _ıuserDal.Delete(user);
            return new SuccessResult(Messages.UserDeleting);
        }

        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<User>>(_ıuserDal.GetAll(), Messages.Maintenance);
            }
            return new SuccessDataResult<List<User>>(_ıuserDal.GetAll(), Messages.UserListing);
        }

        public IDataResult<User> GetById(int Userid)
        {
            return new SuccessDataResult<User>(_ıuserDal.Get(u => u.UserId == Userid));
        }

        public User GetByMail(string email)
        {
            return _ıuserDal.Get(e => e.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _ıuserDal.GetClaims(user);
        }

        public IResult Update(User user)
        {
            _ıuserDal.Update(user);
            return new SuccessResult(Messages.UserUpdating);
        }
    }
}
