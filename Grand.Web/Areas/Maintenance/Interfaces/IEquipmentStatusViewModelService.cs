using Grand.Core;
using Grand.Core.Domain.EquipmentStatusEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.JobStatusEntity;
using Grand.Core.Domain.JobType;
using Grand.Core.Domain.MakerEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IEquipmentStatusViewModelService
    {
        Task<IPagedList<EquipmentStatus>> GetAllEquipmentStatus(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<EquipmentStatus>> GetAllEquipmentStatusAsList(string id);
        Task PrepareEquipmentStatusModel(EquipmentStatusModel model1, object p, bool v);
    }
}
