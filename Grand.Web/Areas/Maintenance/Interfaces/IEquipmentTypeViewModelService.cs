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
    public interface IEquipmentTypeViewModelService
    {
        Task<IPagedList<EquipmentType>> GetAllEquipmentTypes(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<EquipmentType>> GetAllEquipmentTypeAsList(string id);
        
        Task PrepareEquipmentTypeModel(EquipmentTypeModel model1, object p, bool v);
    }
}
