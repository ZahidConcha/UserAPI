using AutoMapper.Configuration;
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
    public class PuestosRepo : IPuestoRepo
    {
        private readonly ApplicationDbContext context;

        public PuestosRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(Puestos entity)
        {
            await context.TPuestos.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Puestos entity)
        {
            context.TPuestos.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exsists(int id)
        {
            return await context.TPuestos.AnyAsync(q => q.Id == id);
        }

        public async Task<IList<Puestos>> FindAll()
        {
            var puestos = await context.TPuestos.Include(q=> q.Departamentos).ToListAsync();
            return puestos;
        }

        public async Task<Puestos> FindById(int Id)
        {
            var puesto = await context.TPuestos.Include(q=> q.Departamentos).FirstOrDefaultAsync(c => c.Id == Id);
            return puesto;
        }

        public async Task<bool> Save()
        {
            var changes = await context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Puestos entity)
        {
            context.TPuestos.Update(entity);
            return await Save();
        }
    }
}
