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

        public Task<bool> Create(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exsists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IdentityUser>> FindAll()
        {
            var users = await Context.Users.ToListAsync();
            return users;
        }

        public Task<IdentityUser> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(IdentityUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
