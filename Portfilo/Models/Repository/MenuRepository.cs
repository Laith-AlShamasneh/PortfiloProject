
using Microsoft.EntityFrameworkCore;
using Portfilo.Data;
using System.Security.Claims;

namespace Portfilo.Models.Repository
{
    public class MenuRepository : IRepository<Menu>
    {
        private readonly AppDbContext db;
        
        public MenuRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Menu entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Menu entity)
        {
            await db.Menus.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Menu entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Menu> FindAsync(int id)
        {
            return await db.Menus.SingleOrDefaultAsync(x => x.MenuId == id);
        }

        public async Task<IEnumerable<Menu>> GetAllForAdminAsync()
        {
            return await db.Menus.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllForClientAsync()
        {
            return await db.Menus.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Menu entity)
        {
            // Ensure the entity is being tracked by the DbContext
            db.Entry(entity).State = EntityState.Modified;

            // Specify the primary key property
            db.Entry(entity).Property(x => x.MenuId).IsModified = false;

            // Save changes to the database
            await db.SaveChangesAsync();
        }
    }
}
