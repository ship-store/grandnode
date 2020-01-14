using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Core;
using Grand.Core.Domain.Equipment;
using Grand.Web.Areas.Maintenance.DomainModels;
namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IEquipmentViewModelService
    {
     // Task PrepareEquipmentModel(EquipmentModel model1, object p, bool v);
        Task PrepareEquipmentModel(EquipmentView equipmentview, string v1, bool v2);
        Task PrepareEquipmentModel(EquipmentModel equipmentModel, object p, bool v);
        Task PrepareEquipmentModel(EquipmentModel equipmentModel, string v1, bool v2);
        Task<IPagedList<Equipment>> GetAllEquipment(string name = "",
           int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
    }
}
