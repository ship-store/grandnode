using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Web.Areas.Maintenance.DomainModels;
namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IEquipmentViewModelService
    {
        Task PrepareEquipmentModel(EquipmentModel model1, object p, bool v);
    }
}
