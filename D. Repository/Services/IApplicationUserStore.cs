using C._Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D._Repository.Services
{
    public interface IApplicationUserStore<TUser> : IUserStore<TUser> where TUser : class
    {
        Task<User> FindByEmail(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IdentityResult> UpdateTokenAsync(User user);
    }
}
