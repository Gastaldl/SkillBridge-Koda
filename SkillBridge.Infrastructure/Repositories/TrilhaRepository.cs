using Microsoft.EntityFrameworkCore;
using SkillBridge.Domain;
using SkillBridge.Domain.Interfaces;
using SkillBridge.Infrastructure.Data;

namespace SkillBridge.Infrastructure.Repositories
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly AppDbContext _context;

        public TrilhaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trilha>> GetAllAsync()
        {
            return await _context.Trilhas.ToListAsync();
        }

        public async Task<Trilha?> GetByIdAsync(long id)
        {
            return await _context.Trilhas.FindAsync(id);
        }

        public async Task<Trilha> AddAsync(Trilha trilha)
        {
            _context.Trilhas.Add(trilha);
            await _context.SaveChangesAsync();
            return trilha;
        }

        public async Task UpdateAsync(Trilha trilha)
        {
            _context.Entry(trilha).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var trilha = await _context.Trilhas.FindAsync(id);
            if (trilha != null)
            {
                _context.Trilhas.Remove(trilha);
                await _context.SaveChangesAsync();
            }
        }
    }
}