using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class CallToActionRepository : IRepository<CallToAction>
    {
        private readonly AppDbContext db;

        public CallToActionRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, CallToAction entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(CallToAction entity)
        {
            await db.CallToActions.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, CallToAction entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<CallToAction> FindAsync(int id)
        {
            return await db.CallToActions.SingleOrDefaultAsync(x => x.CallToActionId == id);
        }

        public async Task<IEnumerable<CallToAction>> GetAllForAdminAsync()
        {
            return await db.CallToActions.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<CallToAction>> GetAllForClientAsync()
        {
            return await db.CallToActions.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, CallToAction entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
