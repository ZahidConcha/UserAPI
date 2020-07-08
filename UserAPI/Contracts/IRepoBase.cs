using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Contracts
{
    public interface IRepoBase<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int Id);
        Task<bool> Exsists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
