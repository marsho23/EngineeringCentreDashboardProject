using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
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

        public async Task<UserLogin> Get(int id)
        {
            return await _engineeringCentreDashboardDbContext.UserLogins.FindAsync(id);
        }

        public async Task<UserLogin> Add(UserLogin userLogin)
        {
            await _engineeringCentreDashboardDbContext.UserLogins.AddAsync(userLogin);
            await _engineeringCentreDashboardDbContext.SaveChangesAsync();
            return userLogin;
        }

        public async Task<IEnumerable<UserLogin>> GetAll()
        {
            return await _engineeringCentreDashboardDbContext.UserLogins.ToListAsync();
        }

        public async Task<UserLogin> Update(UserLogin userLogin)
        {
            _engineeringCentreDashboardDbContext.UserLogins.Update(userLogin);
            await _engineeringCentreDashboardDbContext.SaveChangesAsync();
            return userLogin;
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
