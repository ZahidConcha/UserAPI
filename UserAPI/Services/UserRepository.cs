using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Contracts;
using UserAPI.Data;


namespace UserAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext Context;
        public UserRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IList<IdentityUser>> FindAll()
        {
            var user = await Context.Users.ToListAsync();
            return user;
        }
    }
}
