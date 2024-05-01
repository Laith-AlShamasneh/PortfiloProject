using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class AboutRepository : IRepository<About>
    {
        private readonly AppDbContext db;

        public AboutRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, About entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(About entity)
        {
            await db.Abouts.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, About entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<About> FindAsync(int id)
        {
            return await db.Abouts.SingleOrDefaultAsync(x => x.AboutId == id);
        }

        public async Task<IEnumerable<About>> GetAllForAdminAsync()
        {
            return await db.Abouts.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<About>> GetAllForClientAsync()
        {
            return await db.Abouts.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, About entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
