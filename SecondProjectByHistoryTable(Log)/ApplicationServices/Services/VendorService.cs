﻿using FirstProject.DTOs.Vendors;
using Newtonsoft.Json;
using SecondProjectByHistoryTable_Log_.ApplicationServices.IServices;
using SecondProjectByHistoryTable_Log_.InferaStructure.IRepositories;
using SecondProjectByHistoryTable_Log_.Models;
using SecondProjectEFCoreAttributes.DTOs.Tags;
using SecondProjectEFCoreAttributes.DTOs.Vendors;
using SecondProjectEFCoreAttributes.InferaStructure.IRepositories;
using SecondProjectEFCoreAttributes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SecondProjectByHistoryTable_Log_.ApplicationServices.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _repository;
        private readonly IHistoryRepository _historyRepository;

        public VendorService(IVendorRepository repository, IHistoryRepository historyRepository)
        {
            _repository = repository;
            _historyRepository = historyRepository;
        }

        public async Task<VendorDTO> GetVendorById(int id)
        {
            var data = await _repository.GetVendorById(id);

            var listDTO = data.Tags.Select(x => new TagDTO
            {
                Name = x.Name,
                Value = x.Value
            }).ToList();

            VendorDTO vendorDTO = new VendorDTO
            {
                Name = data.Name,
                Title = data.Title,
                Date = data.Date,
                Tags = listDTO
            };

            return vendorDTO;
        }
        public async Task<bool> DeleteVendorById(int id)
        {
            bool deleted = false;

            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var deleteVendor = await _repository.DeleteById(id);

            if (deleteVendor != null)
            {
                deleted = true;
            }

            var vendorDeleteDTO = new VendorUpdateDTO
            {
                Id = deleteVendor.Id,
                Name = deleteVendor.Name,
                Title = deleteVendor.Title,
                Date = deleteVendor.Date
            };
            string json = JsonConvert.SerializeObject(vendorDeleteDTO);

            var history = new History()
            {
                VendorId = vendorDeleteDTO.Id,
                Operation = "Delete",
                JsonResult = json,
            };

            await _historyRepository.Insert(history);
            scope.Complete();
            return deleted;
        }
        public async Task<int> Update(VendorUpdateDTO DTO, int Id)
        {
            var list = new List<Tag>();
            foreach (var item in DTO.Tags)
            {
                Tag tag = new Tag
                {
                    Name = item.Name,
                    Value = item.Value
                };
                list.Add(tag);
            }

            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var ven = await _repository.GetVendorById(Id);
            ven.Name = DTO.Name;
            ven.Title = DTO.Title;
            ven.Date = DTO.Date;
            ven.Tags = list;

            var vendorUpdateDTO = new VendorUpdateDTO
            {
                Id = ven.Id,
                Name = ven.Name,
                Title = ven.Title,
                Date = ven.Date,
                Tags = ven.Tags.Select(x => new TagDTO
                {
                    Name = x.Name,
                    Value = x.Value

                }).ToList()
            };

            string json = JsonConvert.SerializeObject(vendorUpdateDTO);

            var history = new History()
            {
                VendorId = vendorUpdateDTO.Id,
                Operation = "Put",
                JsonResult = json,
            };

            await _historyRepository.Insert(history);

            var updateVendor = await _repository.Update(ven);

            scope.Complete();

            return updateVendor;
        }
        public async Task<VendorInsertResponseDTO> Insert(VendorInsertDTO dto)
        {
            var tagList = new List<Tag>();
            if (dto.Tags != null && dto.Tags.Count > 0)
            {
                foreach (var Tag in dto.Tags)
                {
                    var tag = new Tag()
                    {
                        Name = Tag.Name,
                        Value = Tag.Value
                    };
                    tagList.Add(tag);
                }
            }

            var vendor = new Vendor()
            {
                Name = dto.Name,
                Title = dto.Title,
                Date = dto.Date,
                Tags = tagList
            };

            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var vendorResult = await _repository.Insert(vendor);

            VendorInsertResponseDTO vendorDTO = new VendorInsertResponseDTO()
            {
                Id = vendorResult.Id,
                Name = vendorResult.Name,
                Title = vendorResult.Title,
                Date = vendorResult.Date,
                Tags = vendorResult.Tags.Select(x => new TagDTO
                {
                    Name = x.Name,
                    Value = x.Value

                }).ToList()
            };

            string json = JsonConvert.SerializeObject(vendorDTO);

            var history = new History()
            {
                VendorId = vendorDTO.Id,
                Operation = "Post",
                JsonResult = json,
            };

            await _historyRepository.Insert(history);
            scope.Complete();

            return vendorDTO;
        }
    }
}
