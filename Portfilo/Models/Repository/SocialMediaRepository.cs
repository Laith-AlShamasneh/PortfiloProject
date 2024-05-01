using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class SocialMediaRepository : IRepository<SocialMedia>
    {
        private readonly AppDbContext db;

        public SocialMediaRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, SocialMedia entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(SocialMedia entity)
        {
            await db.SocialMedias.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, SocialMedia entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<SocialMedia> FindAsync(int id)
        {
            return await db.SocialMedias.SingleOrDefaultAsync(x => x.SocialMediaId == id);
        }

        public async Task<IEnumerable<SocialMedia>> GetAllForAdminAsync()
        {
            return await db.SocialMedias.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<SocialMedia>> GetAllForClientAsync()
        {
            return await db.SocialMedias.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, SocialMedia entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
