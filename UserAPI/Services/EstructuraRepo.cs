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
    public class EstructuraRepo : IEstructuraRepo
    {
        private readonly ApplicationDbContext context;

        public EstructuraRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(Estructura entity)
        {
            await context.TEstructuras.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Estructura entity)
        {
            context.TEstructuras.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exsists(int id)
        {
            return await context.TEstructuras.AnyAsync(c => c.Id == id);
        }

        public async Task<IList<Estructura>> FindAll()
        {
            var empleados = await context.TEstructuras.
                Include(q=>q.Departamento).Include(q=>q.Empleado).Include(q=>q.Sitio).Include(q => q.Puesto).ToListAsync();

            return empleados;
        }

        public async Task<Estructura> FindById(int Id)
        {
            var empleado = await context.TEstructuras.FirstAsync(c=>c.Id == Id);
            return empleado;
        }

        public async Task<bool> Save()
        {
            var changes = await context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Estructura entity)
        {
            context.TEstructuras.Update(entity);
            return await Save();
        }
    }
}
