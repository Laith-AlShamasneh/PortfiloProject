using Microsoft.EntityFrameworkCore;
using Portfilo.Data;

namespace Portfilo.Models.Repository
{
    public class ProfessionalSkillRepository : IRepository<ProfessionalSkill>
    {
        private readonly AppDbContext db;

        public ProfessionalSkillRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task ActiveAsync(int id, ProfessionalSkill entity)
        {
            var data = await FindAsync(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            await UpdateAsync(id, data);
        }

        public async Task AddAsync(ProfessionalSkill entity)
        {
            await db.ProfessionalSkills.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, ProfessionalSkill entity)
        {
            var data = await FindAsync(id);
            data.IsDelete = true;
            await UpdateAsync(id, data);
        }

        public async Task<ProfessionalSkill> FindAsync(int id)
        {
            return await db.ProfessionalSkills.SingleOrDefaultAsync(x => x.ProfessionalSkillId == id);
        }

        public async Task<IEnumerable<ProfessionalSkill>> GetAllForAdminAsync()
        {
            return await db.ProfessionalSkills.Where(x => !x.IsDelete).ToListAsync();
        }

        public async Task<IEnumerable<ProfessionalSkill>> GetAllForClientAsync()
        {
            return await db.ProfessionalSkills.Where(x => !x.IsDelete && x.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(int id, ProfessionalSkill entity)
        {
            var data = await FindAsync(id);

            db.Entry(data).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();
        }
    }
}
