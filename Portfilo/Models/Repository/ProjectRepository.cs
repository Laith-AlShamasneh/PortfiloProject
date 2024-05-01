using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly AppDbContext db;

        public ProjectRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, Project entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(Project entity)
        {
            await db.Projects.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, Project entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<Project> FindAsync(int id)
        {
            return await db.Projects.SingleOrDefaultAsync(x => x.ProjectId == id);
        }

        public async Task<IEnumerable<Project>> GetAllForAdminAsync()
        {
            return await db.Projects.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllForClientAsync()
        {
            return await db.Projects.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, Project entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
