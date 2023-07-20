using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringCentreDashboard.Business
{
    public class UserLoginHelper : IUserLoginHelper
    {
        private readonly EngineeringDashboardDbContext _engineeringCentreDashboardDbContext;

        public UserLoginHelper(EngineeringDashboardDbContext engineeringCentreDashboardDbContext)
        {
            _engineeringCentreDashboardDbContext = engineeringCentreDashboardDbContext;
        }

        public async Task<UserLoginRequest> Get(int id)
        {
            UserLogin userLogin= await _engineeringCentreDashboardDbContext.UserLogins.FindAsync(id);
            return new UserLoginRequest(userLogin);
        }

        public async Task<UserLoginRequest> Add(UserLoginRequest userLogin)
        {
            UserLogin userLoginDbModel = new UserLogin(userLogin);
            await _engineeringCentreDashboardDbContext.UserLogins.AddAsync(userLoginDbModel);
            await _engineeringCentreDashboardDbContext.SaveChangesAsync();
            return new UserLoginRequest(userLoginDbModel);
        }

        public async Task<IEnumerable<UserLoginRequest>> GetAll()
        {
            List<UserLogin> userLogins = await _engineeringCentreDashboardDbContext.UserLogins.ToListAsync();
            return userLogins.Select(userLogin => new UserLoginRequest(userLogin));
        }

        public async Task<UserLoginRequest> Update(UserLoginRequest userLogin)
        {
            UserLogin userLoginDbModel = await _engineeringCentreDashboardDbContext.UserLogins.FindAsync(userLogin.Id);
            if (userLoginDbModel != null)
            {
                userLoginDbModel.Username = userLogin.Username;
                userLoginDbModel.Password = userLogin.Password;
                userLoginDbModel.Email = userLogin.Email;

                _engineeringCentreDashboardDbContext.UserLogins.Update(userLoginDbModel);
                await _engineeringCentreDashboardDbContext.SaveChangesAsync();
            }

            return new UserLoginRequest(userLoginDbModel);
        }


        public async Task Delete(int id)
        {
            var userLogin = await _engineeringCentreDashboardDbContext.UserLogins.FindAsync(id);
            if (userLogin != null)
            {
                _engineeringCentreDashboardDbContext.UserLogins.Remove(userLogin);
                await _engineeringCentreDashboardDbContext.SaveChangesAsync();
            }
        }

        public UserLogin GetUserLoginById(int userLoginId)
        {
            var userLogin = _engineeringCentreDashboardDbContext.UserLogins.FirstOrDefault(u => u.Id == userLoginId);
            return userLogin;
        }
    }
}
