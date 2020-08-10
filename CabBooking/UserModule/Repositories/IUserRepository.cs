using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModule.Models;

namespace UserModule.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserRegister(User users);
        Task<Login> UserLogin(string uname,string pwd);
        Task<User> GetProfile(int userid);
        Task<bool> UpdateProfile(User users);
        Task<bool> DeleteProfile(int userid);
    }
}
