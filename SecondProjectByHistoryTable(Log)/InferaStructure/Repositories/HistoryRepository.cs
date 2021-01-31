using SecondProjectByHistoryTable_Log_.InferaStructure.IRepositories;
using SecondProjectByHistoryTable_Log_.Models;
using SecondProjectEFCoreAttributes.Contexts;
using SecondProjectEFCoreAttributes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.InferaStructure.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ProjectContext _db;

        public HistoryRepository(ProjectContext db)
        {
            _db = db;
        }

        public async Task<int> Insert(History history)
        {
            await _db.History.AddAsync(history);
            return await _db.SaveChangesAsync();
        }
    }
}
