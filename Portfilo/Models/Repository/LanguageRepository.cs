using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class LanguageRepository : IRepository<Language>
    {
        private readonly AppDbContext db;

        public LanguageRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Language entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Language entity)
        {
            await db.Languages.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Language entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Language> FindAsync(int id)
        {
            return await db.Languages.SingleOrDefaultAsync(x => x.LanguageId == id);
        }

        public async Task<IEnumerable<Language>> GetAllForAdminAsync()
        {
            return await db.Languages.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Language>> GetAllForClientAsync()
        {
            return await db.Languages.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Language entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
