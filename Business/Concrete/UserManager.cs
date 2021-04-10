using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_ıuserDal.Get(e => e.Email == email));
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

        public IResult UpdateForUser(UpdateForUserDto user)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var userInfo = new User()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _ıuserDal.Update(userInfo);
            return new SuccessResult();
        }
    }
}
