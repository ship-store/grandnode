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
using Grand.Services.Cbm;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class CbmViewModelService : ICbmViewModelService
    {
        private readonly ICbmService _cbmService;
        public CbmViewModelService(ICbmService _CbmService)
        {
            this._cbmService = _CbmService;
            
        }
        Task<IPagedList<Grand.Core.Domain.CbmEntity.CBM>> ICbmViewModelService.GetAllCbm(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Grand.Core.Domain.CbmEntity.CBM>> ICbmViewModelService.GetAllCbmAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task ICbmViewModelService.PrepareCbmModel(CBMModel addNewCbm, object p, bool v)
        {
            try
            {

                var cbm = new CBM();

                cbm.Cbm_Name = addNewCbm.CBM_Name;
               
                await _cbmService.InsertCbm(cbm);
            }
            catch (Exception ex)
            {
                var cbm = new CBM();

                cbm.Cbm_Name = addNewCbm.CBM_Name;

                await _cbmService.InsertCbm(cbm);

            }
        }
    }
}
