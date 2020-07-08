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
    public class EmpleadoRepo : IEmpleadosRepo
    {
        private readonly ApplicationDbContext context;

        public EmpleadoRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(Empleado entity)
        {
            await context.TEmpleados.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Empleado entity)
        {
            context.TEmpleados.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exsists(int id)
        {
            return await context.TEmpleados.AnyAsync(c => c.Id == id);
        }

        public async Task<IList<Empleado>> FindAll()
        {
            var empleados = await context.TEmpleados.ToListAsync();
            return empleados;
        }

        public async Task<Empleado> FindById(int Id)
        {
            var empleado = await context.TEmpleados.FirstAsync(c => c.Id == Id);
            return empleado;
        }

        public async Task<bool> Save()
        {
            var changes = await context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Empleado entity)
        {
            context.TEmpleados.Update(entity);
            return await Save();
        }
    }
}
