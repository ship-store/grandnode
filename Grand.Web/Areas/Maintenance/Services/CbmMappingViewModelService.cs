using Grand.Core;
using Grand.Core.Domain.CbmEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.EquipmentType;
using Grand.Services.JobType;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Services.CbmMapping;
using Grand.Core.Data;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class CbmMappingViewModelService : ICbmMappingViewModelService
    {
        private readonly ICbmMappingService _cbmMappingService;
        private readonly IRepository<CBMMapping> _cbmMappingRepository;
        public CbmMappingViewModelService(ICbmMappingService _CbmMappingService,
            IRepository<CBMMapping> _cbmMappingRepository)
        {
            this._cbmMappingService = _CbmMappingService;
            this._cbmMappingRepository = _cbmMappingRepository;
        }

        public async Task<IPagedList<CBMMapping>> GetAll()
        {
            var query = _cbmMappingRepository.Table;

            return await PagedList<CBMMapping>.Create(query, 0, 500);
        }

        async Task<IPagedList<CBMMapping>> ICbmMappingViewModelService.GetAllCbmMapping(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _cbmMappingRepository.Table;

            return await PagedList<CBMMapping>.Create(query, pageIndex, pageSize);
           
        }

       


        async Task<IPagedList<Grand.Core.Domain.CbmEntity.CBMMapping>> ICbmMappingViewModelService.GetAllCbmMappingAsList(string id)
        {
            var query = _cbmMappingRepository.Table;

            return await PagedList<CBMMapping>.Create(query, 0,500);
        }

        async Task ICbmMappingViewModelService.PrepareCbmMappingModel(CBMMappingModel addNewCbmMapping, object p, bool v)
        {
            try
            {

                var cbmMapping = new CBMMapping();

                cbmMapping.Cbm_Name = addNewCbmMapping.Cbm_Name;
                cbmMapping.equipmentComponent = addNewCbmMapping.equipmentComponent;
                cbmMapping.jobCode = addNewCbmMapping.jobCode;

                await _cbmMappingService.InsertCbmMapping(cbmMapping);
            }
            catch (Exception ex)
            {
                var cbmMapping = new CBMMapping();

                cbmMapping.Cbm_Name = addNewCbmMapping.Cbm_Name;
                cbmMapping.equipmentComponent = addNewCbmMapping.equipmentComponent;
                cbmMapping.jobCode = addNewCbmMapping.jobCode;

                await _cbmMappingService.InsertCbmMapping(cbmMapping);

            }
        }
    }
}
