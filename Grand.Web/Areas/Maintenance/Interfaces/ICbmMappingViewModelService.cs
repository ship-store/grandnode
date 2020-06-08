using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface ICbmMappingViewModelService
    {
        Task<IPagedList<Core.Domain.CbmEntity.CBMMapping>> GetAllCbmMapping(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Task<IPagedList<Core.Domain.CbmEntity.CBMMapping>> GetAll();
       
        Task<IPagedList<Core.Domain.CbmEntity.CBMMapping>> GetAllCbmMappingAsList(string id);
        Task PrepareCbmMappingModel(CBMMappingModel model1, object p, bool v);
        
    }
}
