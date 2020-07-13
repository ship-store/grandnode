using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.ReportedBy
{
    public interface IReportedByService
    {
         Task<IPagedList<Grand.Core.Domain.ReportedByEntity.ReportedBy>> GetAllReportedBy(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.ReportedByEntity.ReportedBy>> GetAllReportedByAsList();
           Task PrepareReportedByModel(Grand.Core.Domain.ReportedByEntity.ReportedBy model1, object p, bool v);
       
        Task InsertReportedBy(Core.Domain.ReportedByEntity.ReportedBy reportedBy);
        Task UpdateReportedBy(Core.Domain.ReportedByEntity.ReportedBy reportedBy);
        Task<Core.Domain.ReportedByEntity.ReportedBy> GetReportedByById(string Id);
    }
}
