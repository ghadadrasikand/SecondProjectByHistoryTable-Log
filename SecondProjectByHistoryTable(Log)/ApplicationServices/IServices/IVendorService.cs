using FirstProject.DTOs.Vendors;
using SecondProjectByHistoryTable_Log_.DTOs;
using SecondProjectEFCoreAttributes.DTOs.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.ApplicationServices.IServices
{
    public interface IVendorService
    {
        Task<bool> DeleteVendorById(int id);
        Task<VendorDTO> GetVendorById(int id);
        Task<int> Update(VendorUpdateDTO DTO, int Id);
        Task<VendorInsertResponseDTO> Insert(VendorInsertDTO dto);
    }
}
