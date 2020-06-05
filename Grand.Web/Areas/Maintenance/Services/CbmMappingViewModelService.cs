using Grand.Core;
using Grand.Core.Domain.CbmEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobTypeEntity;
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

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class CbmMappingViewModelService : ICbmMappingViewModelService
    {
        private readonly ICbmMappingService _cbmMappingService;
        public CbmMappingViewModelService(ICbmMappingService _CbmMappingService)
        {
            this._cbmMappingService = _CbmMappingService;
            
        }
        Task<IPagedList<Grand.Core.Domain.CbmEntity.CBMMapping>> ICbmMappingViewModelService.GetAllCbmMapping(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Grand.Core.Domain.CbmEntity.CBMMapping>> ICbmMappingViewModelService.GetAllCbmMappingAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task ICbmMappingViewModelService.PrepareCbmMappingModel(CBMMappingModel addNewCbmMapping, object p, bool v)
        {
            try
            {

                var cbmMapping = new CBMMapping();

                cbmMapping.Cbm_Name = addNewCbmMapping.Cbm_Name;
                cbmMapping.equipmentComponent = addNewCbmMapping.equipmentComponent;

                await _cbmMappingService.InsertCbmMapping(cbmMapping);
            }
            catch (Exception ex)
            {
                var cbmMapping = new CBMMapping();

                cbmMapping.Cbm_Name = addNewCbmMapping.Cbm_Name;
                cbmMapping.equipmentComponent = addNewCbmMapping.equipmentComponent;

                await _cbmMappingService.InsertCbmMapping(cbmMapping);

            }
        }
    }
}
