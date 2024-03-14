using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadDoctores.Data;
using NetCoreSeguridadDoctores.Models;

namespace NetCoreSeguridadDoctores.Repositories
{
    public class RepositoryDoctores
    {
        private HospitalContext context;

        public RepositoryDoctores(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            return await this.context.Doctores.ToListAsync();
        }

        public async Task<Doctor> FindDoctorAsync(int idDoctor)
        {
            return await this.context.Doctores
                .FirstOrDefaultAsync(x => x.IdDoctor == idDoctor);
        }

        public async Task<Doctor> LoginDoctorAsync
            (string apellido, int iddoctor)
        {
            Doctor doc = 
                await this.context.Doctores
                .Where(d => d.Apellido == apellido &&
                d.IdDoctor == iddoctor ).FirstOrDefaultAsync();

            return doc;
        }


    }
}
