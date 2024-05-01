using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class HeaderRepository : IRepository<Header>
    {
        private readonly AppDbContext db;

        public HeaderRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Header entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Header entity)
        {
            await db.Headers.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Header entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Header> FindAsync(int id)
        {
            return await db.Headers.SingleOrDefaultAsync(x => x.HeaderId == id);
        }

        public async Task<IEnumerable<Header>> GetAllForAdminAsync()
        {
            return await db.Headers.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Header>> GetAllForClientAsync()
        {
            return await db.Headers.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Header entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
