using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class ExperienceRepository : IRepository<Experience>
    {
        private readonly AppDbContext db;

        public ExperienceRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Experience entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Experience entity)
        {
            await db.Experiences.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Experience entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Experience> FindAsync(int id)
        {
            return await db.Experiences.SingleOrDefaultAsync(x => x.ExperienceId == id);
        }

        public async Task<IEnumerable<Experience>> GetAllForAdminAsync()
        {
            return await db.Experiences.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Experience>> GetAllForClientAsync()
        {
            return await db.Experiences.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Experience entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
