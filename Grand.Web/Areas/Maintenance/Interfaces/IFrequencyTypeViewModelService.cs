using Grand.Core;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.FrequencyTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IFrequencyTypeViewModelService
    {
        Task<IPagedList<FrequencyType>> GetAllFrequencyTypes(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<FrequencyType>> GetAllFrequencyTypeAsList(string id);
        
        Task PrepareFrequencyTypeModel(FrequencyTypeModel model1, object p, bool v);
    }
}
