using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondProjectByHistoryTable_Log_.ApplicationServices.IServices;
using SecondProjectEFCoreAttributes.DTOs.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllById(int id)
        {
            var vendorGetByIdResponse = await _vendorService.GetVendorById(id);
            return Ok(vendorGetByIdResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor([FromRoute] int id)
        {
            var vendorDeleted = await _vendorService.DeleteVendorById(id);
            if (vendorDeleted)
            {
                return Ok($"The Vendor By The Id = {id} was Deleted Successfully !");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, VendorUpdateDTO DTO)
        {
            var vendorUpdate = await _vendorService.Update(DTO, id);
            if (vendorUpdate > 0)
            {
                return Ok($"The Vendor By The Id = {id} was Updated Successfully !");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] VendorInsertDTO dto)
        {
            var insertVendor = await _vendorService.Insert(dto);

            return Created(nameof(GetAllById), insertVendor);
        }
    }
}
