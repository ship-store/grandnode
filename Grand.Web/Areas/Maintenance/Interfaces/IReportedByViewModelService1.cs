using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.ReportedByEntity;
using Grand.Web.Areas.Admin.Interfaces;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface IReportedByViewModelService1
    {
        Task<IPagedList<ReportedBy>> GetAllReportedBy(string name = "",
             int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task<IPagedList<ReportedBy>> GetAllReportedByAsList(string id);
        Task PrepareReportedByModel(ReportedByModel model1, object p, bool v);
    }
}
