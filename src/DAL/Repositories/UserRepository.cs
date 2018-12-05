using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Repositories
{
    public class UserRepository : IRepository<VTSUSER, long>
    {
        public VTSDBEntities ctx = new VTSDBEntities();

        public IEnumerable<VTSUSER> Get()
        {
            return ctx.VTSUSERs;
        }

        public VTSUSER Get(long id)
        {
            return ctx.VTSUSERs.Find(id);
        }

        public bool IsActive(string email)
        {
            var user = ctx.VTSUSERs.Where(x => x.Email.Equals(email)).FirstOrDefault();
            return user.IsActive;
        }

        /// <summary>
        /// Get User by email ID or UserName
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public VTSUSER Get(string email)
        {
            return ctx.VTSUSERs.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public string GetRole(string email)
        {
            var user = ctx.VTSUSERs.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                return user.IsAdmin ? VTSConstants.Admin : VTSConstants.User;
            }
            else { return string.Empty; }
        }


        public bool IsAdmin(string email)
        {
            var user = ctx.VTSUSERs.Where(x => x.Email.Equals(email)).FirstOrDefault();
            return user != null ? user.IsAdmin : false;
        }

        public VTSUSER Create(VTSUSER entity)
        {
            ctx.VTSUSERs.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public bool Update(long id, VTSUSER entity)
        {
            var user = ctx.VTSUSERs.Find(id);
            return SaveChangesForUpdateProfile(user, entity);
        }

        bool SaveChangesForUpdateProfile(VTSUSER UpdateTo, VTSUSER UpdateFrom)
        {
            try
            {
                UpdateTo.FirstName = UpdateFrom.FirstName;
                UpdateTo.LastName = UpdateFrom.LastName;
                UpdateTo.Password = UpdateFrom.Password ?? UpdateTo.Password;
                UpdateTo.Email = UpdateFrom.Email ?? UpdateTo.Email;
                UpdateTo.Phone = UpdateFrom.Phone;
                UpdateTo.City = UpdateFrom.City;
                UpdateTo.State = UpdateFrom.State;
                UpdateTo.Country = UpdateFrom.Country;
                UpdateTo.Pincode = UpdateFrom.Pincode;
                UpdateTo.IsActive = UpdateFrom.IsActive;
                UpdateTo.LastUpdatePassword = DateTime.UtcNow;
                UpdateTo.Address = UpdateFrom.Address;
                UpdateTo.Photo = UpdateFrom.Photo ?? UpdateTo.Photo;
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(long id)
        {
            var delUser = ctx.VTSUSERs.Find(id);
            if (delUser != null)
            {
                ctx.VTSUSERs.Remove(delUser);
                ctx.SaveChanges();
                return true;
            }
            else
                return true;
        }

        public VTSUSER Authenticate(string Email, string Password)
        {
            return ctx.VTSUSERs.Where(x => x.Email.ToLower().Equals(Email.ToLower())
                                      && x.Password.Equals(Password)
                                      && x.IsActive).FirstOrDefault();
        }

        public void Dispose()
        {
            if (ctx != null)
                ctx.Dispose();
        }

        public bool Update(string email, VTSUSER entity)
        {
            var user = Get(email);
            return SaveChangesForUpdateProfile(user, entity);
        }

        public bool ActivateAccount(long userID)
        {
            var user = Get(userID);
            if (user != null)
            {
                user.IsActive = true;
                ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool ChangePassword(string email, string password)
        {
            var user = Get(email);
            if (user != null)
            {
                user.Password = password;
                ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
