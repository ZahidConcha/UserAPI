using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Contracts;
using UserAPI.Data;
using UserAPI.Modals;

namespace UserAPI.Services
{
    public class SitiosRepo : ISitiosRepo
    {
        private readonly ApplicationDbContext context;
        public SitiosRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(Sitios entity)
        {
            await context.TSitios.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Sitios entity)
        {
            context.TSitios.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exsists(int id)
        {
            return await context.TSitios.AnyAsync(c => c.Id == id);
        }

        public async Task<IList<Sitios>> FindAll()
        {
            var Sitioss = await context.TSitios.ToListAsync();
            return Sitioss;
        }

        public async Task<Sitios> FindById(int Id)
        {
            var Sitios = await context.TSitios.FirstOrDefaultAsync(c => c.Id == Id);
            return Sitios;
        }

        public async Task<bool> Save()
        {
            var changes = await context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Sitios entity)
        {
            context.TSitios.Update(entity);
            return await Save();
        }
    }
}
