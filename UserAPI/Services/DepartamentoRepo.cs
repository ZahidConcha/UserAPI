﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Contracts;
using UserAPI.Data;
using UserAPI.Modals;

namespace UserAPI.Services
{
    public class DepartamentoRepo : IDepartamentoRepo
    {
        private readonly ApplicationDbContext context;

        public DepartamentoRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(Departamentos entity)
        {
            await context.TDepartamentos.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Departamentos entity)
        {
            context.TDepartamentos.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exsists(int id)
        {
            return await context.TDepartamentos.AnyAsync(c => c.Id == id);
        }

        public async Task<IList<Departamentos>> FindAll()
        {
            //var departamentos = await context.TDepartamentos.Include(q => q.ListPuesto).ToListAsync();
            var departamentos = await context.TDepartamentos.ToListAsync();
            return departamentos;
        }

        public async Task<Departamentos> FindById(int Id)
        {
            //var departamento = await context.TDepartamentos.Include(q=>q.ListPuesto).FirstOrDefaultAsync(c => c.Id == Id);
            var departamento = await context.TDepartamentos.FirstOrDefaultAsync(c => c.Id == Id);
            return departamento;
        }

        public async Task<bool> Save()
        {
            var changes = await context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Departamentos entity)
        {
            context.TDepartamentos.Update(entity);
            return await Save();
        }
    }
}
