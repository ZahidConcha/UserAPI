using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Contracts
{
    public interface IRepoBase<T> where T : class
    {
        Task<IList<T>> FindAll();
    }
}
