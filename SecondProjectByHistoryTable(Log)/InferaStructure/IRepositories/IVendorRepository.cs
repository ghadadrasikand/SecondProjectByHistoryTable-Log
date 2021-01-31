using SecondProjectEFCoreAttributes.Models;
using System.Threading.Tasks;

namespace SecondProjectEFCoreAttributes.InferaStructure.IRepositories
{
    public interface IVendorRepository
    {
        Task<Vendor> DeleteById(int id);
        Task<Vendor> GetVendorById(int id);
        Task<int> Update(Vendor vendor);
        Task<Vendor> Insert(Vendor vendor);
        Task<int> SaveChangeAsync();
    }
}
