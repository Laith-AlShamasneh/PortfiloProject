using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class EducationRepository : IRepository<Education>
    {
        private readonly AppDbContext db;

        public EducationRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Education entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Education entity)
        {
            await db.Educations.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Education entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Education> FindAsync(int id)
        {
            return await db.Educations.SingleOrDefaultAsync(x => x.EducationId == id);
        }

        public async Task<IEnumerable<Education>> GetAllForAdminAsync()
        {
            return await db.Educations.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Education>> GetAllForClientAsync()
        {
            return await db.Educations.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Education entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
