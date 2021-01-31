using SecondProjectByHistoryTable_Log_.Models;
using SecondProjectEFCoreAttributes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.InferaStructure.IRepositories
{
    public interface IHistoryRepository
    {
        Task<int> Insert(History history);
    }
}
