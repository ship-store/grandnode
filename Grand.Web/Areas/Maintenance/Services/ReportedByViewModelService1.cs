using Grand.Core;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Core.Domain.ReportedByEntity;
using Grand.Services.EquipmentType;
using Grand.Services.Maker;
using Grand.Services.ReportedBy;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class ReportedByViewModelService1 : IReportedByViewModelService1
    {
        private readonly IReportedByService _reportedByService;
        public ReportedByViewModelService1(IReportedByService _reportedByService)
        {
            this._reportedByService = _reportedByService;
            
        }
        Task<IPagedList<ReportedBy>> IReportedByViewModelService1.GetAllReportedBy(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<ReportedBy>> IReportedByViewModelService1.GetAllReportedByAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IReportedByViewModelService1.PrepareReportedByModel(ReportedByModel addNewReportedBy, object p, bool v)
        {
            try
            {

                var reportedBy = new ReportedBy();

                reportedBy.Reported_By = addNewReportedBy.Reported_By;
               
                await  _reportedByService.InsertReportedBy(reportedBy);
            }
            catch (Exception ex)
            {
                var reportedBy = new ReportedBy();

                reportedBy.Reported_By = addNewReportedBy.Reported_By;

                await _reportedByService.InsertReportedBy(reportedBy);

            }
        }
    }
}
