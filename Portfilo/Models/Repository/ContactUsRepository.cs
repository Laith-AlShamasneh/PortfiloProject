using Portfilo.Data;
using Microsoft.EntityFrameworkCore;

namespace Portfilo.Models.Repository
{
    public class ContactUsRepository : IRepository<TransactionContactUs>
    {
        private readonly AppDbContext db;

        public ContactUsRepository(AppDbContext db)
        {
            this.db = db;
        }

        public Task ActiveAsync(int id, TransactionContactUs entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(TransactionContactUs entity)
        {
            await db.TransactionsContactUs.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public Task DeleteAsync(int id, TransactionContactUs entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionContactUs> FindAsync(int id)
        {
            return await db.TransactionsContactUs.SingleOrDefaultAsync(x => x.TransactionContactUsId == id);
        }

        public async Task<IEnumerable<TransactionContactUs>> GetAllForAdminAsync()
        {
            return await db.TransactionsContactUs.ToListAsync();
        }

        public Task<IEnumerable<TransactionContactUs>> GetAllForClientAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, TransactionContactUs entity)
        {
            throw new NotImplementedException();
        }
    }
}
