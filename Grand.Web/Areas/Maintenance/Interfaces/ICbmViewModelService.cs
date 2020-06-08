using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface ICbmViewModelService
    {
        Task<IPagedList<Core.Domain.CbmEntity.CBM>> GetAllCbm(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<Core.Domain.CbmEntity.CBM>> GetAllCbmAsList(string id);
        Task PrepareCbmModel(CBMModel model1, object p, bool v);
      
        
    }
}
