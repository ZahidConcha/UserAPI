using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Contracts
{
    public interface IUserRepository : IRepoBase<IdentityUser> 
    { 
    }
}
