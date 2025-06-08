using FinBeatTechTestTask.DAL.Interfaces;
using FinBeatTechTestTask.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.DAL.Repositories
{
    public class PairValueRepository : IBaseRepository<PairValueEntity>
    {
        private readonly AppDbContext _appDbContext;

        public PairValueRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<PairValueEntity> GetAll()
        {
            return _appDbContext.pairValues;
        }
        public async Task Clear()
        {
            var allEntities = await _appDbContext.pairValues.ToListAsync();
            _appDbContext.pairValues.RemoveRange(allEntities);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddRange(IEnumerable<PairValueEntity> entities)
        {
            await _appDbContext.pairValues.AddRangeAsync(entities);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
