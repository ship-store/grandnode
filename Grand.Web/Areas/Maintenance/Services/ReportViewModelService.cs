using Grand.Core;
using Grand.Core.Domain.BreakdownJobReport;
using Grand.Core.Domain.DueJobReport;
using Grand.Core.Domain.Report;
using Grand.Core.Domain.UnplannedJobs;
using Grand.Services.Report;
using Grand.Services.UnplannedJobs;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class ReportViewModelService : IReportViewModelService
    {
        private readonly IUnplannedJobService _unplannedJobService;
        private readonly IReportService _reportService;
        public ReportViewModelService(IUnplannedJobService _unplannedJobService, IReportService _reportService)
        {
            this._reportService = _reportService;
            this._unplannedJobService = _unplannedJobService;
        }

        //Task<IPagedList<UnplannedJob>> IUnplannedJobViewModelService.GetAllUnplannedJobs(string name, int pageIndex, int pageSize, bool showHidden)
        //{
        //    throw new NotImplementedException();
        //}
        //Task<IPagedList<UnplannedJob>> IUnplannedJobViewModelService.GetAllUnplannedJobsAsList(string id)
        //{
        //    throw new NotImplementedException();
        //}
        public virtual async Task PrepareReportModel(ReportModel model1, object p, bool v)
        {
            try
            {

                var report = new Report();

                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.Title = model1.Title;
                report.JobReportedDate = model1.JobReportedDate;
                report.ReportedBy = model1.ReportedBy;
                report.Status = model1.Status;
                report.Vessel = model1.Vessel;
                report.DeleteStatus = null;
                report.Category = model1.Category;
                report.Remark = model1.Remark;
                report.JobCompletedDate = model1.JobCompletedDate;

                await _reportService.InsertReport(report);
               
            }
            catch (Exception)
            {
                var report = new Report();

                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.Title = model1.Title;
                report.JobReportedDate = model1.JobReportedDate;
                report.ReportedBy = model1.ReportedBy;
                report.Status = model1.Status;
                report.Vessel = model1.Vessel;
                report.DeleteStatus = null;
                report.Category = model1.Category;
                report.Remark = model1.Remark;
                report.JobCompletedDate = model1.JobCompletedDate;

                await _reportService.InsertReport(report);

            }
        }

        public virtual async Task PrepareDueJobReportModel(DueJobReportModel model1, object p, bool v)
        {
            try
            {

                var report = new DueJobReport();
                report.EquipmentCode = model1.EquipmentCode;
                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.JobTitle = model1.JobTitle;
                report.JobDescription = model1.JobDescription;
                report.CalFrequency = model1.CalFrequency;
                report.FrequencyType = model1.FrequencyType;
                report.Vessel = model1.Vessel;
                report.Department = model1.Department;
                report.AssignedTo = model1.AssignedTo;
                report.NEXT_DUE_DATE = model1.NEXT_DUE_DATE;
                report.LAST_DONE_DATE = model1.LAST_DONE_DATE;
                report.Job_Type = model1.Job_Type;
                report.Maintenance_Type = model1.Maintenance_Type;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.JobStatus = model1.JobStatus;
                report.JobOrder = model1.JobOrder;
                report.Reading = model1.Reading;
                report.Remark = model1.Remark;
                report.Status = "Completed";
                report.Vessel = model1.Vessel;

                await _reportService.InsertDueJobReport(report);

            }
            catch (Exception)
            {
                var report = new DueJobReport();
                report.EquipmentCode = model1.EquipmentCode;
                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.JobTitle = model1.JobTitle;
                report.JobDescription = model1.JobDescription;
                report.CalFrequency = model1.CalFrequency;
                report.FrequencyType = model1.FrequencyType;
                report.Vessel = model1.Vessel;
                report.Department = model1.Department;
                report.AssignedTo = model1.AssignedTo;
                report.NEXT_DUE_DATE = model1.NEXT_DUE_DATE;
                report.LAST_DONE_DATE = model1.LAST_DONE_DATE;
                report.Job_Type = model1.Job_Type;
                report.Maintenance_Type = model1.Maintenance_Type;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.JobStatus = model1.JobStatus;
                report.JobOrder = model1.JobOrder;
                report.Reading = model1.Reading;
                report.Status = model1.Status;
                report.Status = "Completed";
                await _reportService.InsertDueJobReport(report);

            }
        }

        public virtual async Task PreparePostponedJobReportModel(DueJobReportModel model1, object p, bool v)
        {
            try
            {

                var report = new DueJobReport();
                report.EquipmentCode = model1.EquipmentCode;
                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.JobTitle = model1.JobTitle;
                report.JobDescription = model1.JobDescription;
                report.CalFrequency = model1.CalFrequency;
                report.FrequencyType = model1.FrequencyType;
                report.Vessel = model1.Vessel;
                report.Department = model1.Department;
                report.AssignedTo = model1.AssignedTo;
                report.NEXT_DUE_DATE = model1.NEXT_DUE_DATE;
                report.LAST_DONE_DATE = model1.LAST_DONE_DATE;
                report.Job_Type = model1.Job_Type;
                report.Maintenance_Type = model1.Maintenance_Type;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.JobStatus = model1.JobStatus;
                report.JobOrder = model1.JobOrder;
                report.Reading = model1.Reading;
                report.Remark = model1.Remark;
                report.Status = "Completed";
                report.Vessel = model1.Vessel;

                await _reportService.InsertDueJobReport(report);

            }
            catch (Exception)
            {
                var report = new DueJobReport();
                report.EquipmentCode = model1.EquipmentCode;
                report.EquipmentName = model1.EquipmentName;
                report.JobOrder = model1.JobOrder;
                report.JobTitle = model1.JobTitle;
                report.JobDescription = model1.JobDescription;
                report.CalFrequency = model1.CalFrequency;
                report.FrequencyType = model1.FrequencyType;
                report.Vessel = model1.Vessel;
                report.Department = model1.Department;
                report.AssignedTo = model1.AssignedTo;
                report.NEXT_DUE_DATE = model1.NEXT_DUE_DATE;
                report.LAST_DONE_DATE = model1.LAST_DONE_DATE;
                report.Job_Type = model1.Job_Type;
                report.Maintenance_Type = model1.Maintenance_Type;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.JobStatus = model1.JobStatus;
                report.JobOrder = model1.JobOrder;
                report.Reading = model1.Reading;
                report.Status = model1.Status;
                report.Status = "Completed";
                await _reportService.InsertDueJobReport(report);

            }
        }
        public virtual async Task PrepareBreakdownJobReportModel(BreakdownJobReportModel model1, object p, bool v)
        {
            try
            {

              var report = new BreakdownJobReport();
                report.EquipmentName = model1.EquipmentName;
               
                report.JobOrder = model1.JobOrder;
                report.Title = model1.Title;
                report.JobReportedDate = model1.JobReportedDate;
                report.ReportedBy = model1.ReportedBy;
                report.Status = model1.Status;

                report.Vessel = model1.Vessel;
                report.DeleteStatus = model1.DeleteStatus;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.Remark = model1.Remark;
                //report.Job_Type = model1.Job_Type;
                //report.Maintenance_Type = model1.Maintenance_Type;
                //report.JobCompletedDate = model1.JobCompletedDate;
                //report.JobStatus = model1.JobStatus;
                //report.JobOrder = model1.JobOrder;
                //report.Reading = model1.Reading;
                //report.Remark = model1.Remark;

                await _reportService.InsertBreakdownJobReport(report);

            }
            catch (Exception)
            {
                var report = new BreakdownJobReport();
                report.EquipmentName = model1.EquipmentName;

                report.JobOrder = model1.JobOrder;
                report.Title = model1.Title;
                report.JobReportedDate = model1.JobReportedDate;
                report.ReportedBy = model1.ReportedBy;
                report.Status = model1.Status;

                report.Vessel = model1.Vessel;
                report.DeleteStatus = model1.DeleteStatus;
                report.JobCompletedDate = model1.JobCompletedDate;
                report.Remark = model1.Remark;

                await _reportService.InsertBreakdownJobReport(report);

            }
        }
        //public async Task DeleteUnplannedJob(UnplannedJob unplannedjob)
        //{
        //    await _unplannedJobService.DeleteUnplannedJob(unplannedjob);
        //}
    }
}


