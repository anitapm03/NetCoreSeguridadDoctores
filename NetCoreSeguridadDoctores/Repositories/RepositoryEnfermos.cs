using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadDoctores.Data;
using NetCoreSeguridadDoctores.Models;

namespace NetCoreSeguridadDoctores.Repositories
{
    public class RepositoryEnfermos
    {
        private HospitalContext context;

        public RepositoryEnfermos(HospitalContext context)
        {
            this.context = context;
        }   

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in context.Enfermos
                           select datos;
            return consulta.ToList();
        }

        public async Task<Enfermo> FindEnfermoAsync(int idEnfermo)
        {
            return await this.context.Enfermos
                .FirstOrDefaultAsync(x => x.IdEnfermo == idEnfermo);
        }

        public async Task EliminarEnfermoAsync(int id)
        {

            var enfermoAEliminar = context.Enfermos
                .FirstOrDefault(e => e.IdEnfermo == id);

            if (enfermoAEliminar != null)
            {
                context.Enfermos.Remove(enfermoAEliminar);
                context.SaveChanges();
            }

        }
    }
}
