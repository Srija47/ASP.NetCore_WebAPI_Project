using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UserModule.Models;

namespace UserModule.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<bool> UserRegister(User users)
        {

            _context.Users.Add(users);
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Login> UserLogin(string uname,string pwd)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Name == uname && e.Password == pwd);
            if (user != null)
            {
                return new Login
                {
                    Name = user.Name,
                    Password = user.Password,
                    Id = user.Id,
                };
            }
            else
            {
                Console.WriteLine("Not valid");
                return null;
            }
        }
        public async Task<User> GetProfile(int userid)
        {
            var users = await _context.Users.FindAsync(userid);
            if (users == null)
                return null;
            else
            {
                return users;
            }
        }
        public async Task<bool> UpdateProfile(User users)
        {
            _context.Users.Update(users);
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> DeleteProfile(int userid)
        {
            var users = await _context.Users.FindAsync(userid);
            if (users != null)
            {
                _context.Remove(users);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
