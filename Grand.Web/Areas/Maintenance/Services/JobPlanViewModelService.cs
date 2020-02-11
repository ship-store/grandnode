using Grand.Core.Domain.Jobplan;
using Grand.Services.Jobplan;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class JobPlanViewModelService : IJobPlanViewModelService
    {
        private readonly IJobplanService _jobPlanService;

        public JobPlanViewModelService(IJobplanService _jobPlanService)
        {
            this._jobPlanService = _jobPlanService;
        }
        async Task IJobPlanViewModelService.PrepareJobplanModel(JobplanModel addNewJobPlan, object p, bool v)
        {
            try
            {
                var jobPlan = new Jobplan();

                jobPlan.EquipmentName = addNewJobPlan.EquipmentName;
                jobPlan.EquipmentCode = addNewJobPlan.EquipmentCode;
                jobPlan.JobOrder = addNewJobPlan.JobOrder;
                jobPlan.CalFrequency = addNewJobPlan.CalFrequency;
                jobPlan.RunFrequency = addNewJobPlan.RunFrequency;
                jobPlan.FrequencyType = addNewJobPlan.FrequencyType;
                jobPlan.JobDescription = addNewJobPlan.JobDescription;
                jobPlan.JobStatus = addNewJobPlan.JobStatus;
                jobPlan.JobTitle = addNewJobPlan.JobTitle;
                jobPlan.LAST_DONE_DATE = addNewJobPlan.LAST_DONE_DATE;
                jobPlan.Maintenance_Type = addNewJobPlan.Maintenance_Type;
                jobPlan.NEXT_DUE_DATE = addNewJobPlan.NEXT_DUE_DATE;
                jobPlan.Priority = addNewJobPlan.Priority;
                jobPlan.Rank = addNewJobPlan.Rank;
                jobPlan.Department = addNewJobPlan.Department;
                jobPlan.AssignedTo = addNewJobPlan.AssignedTo;
                jobPlan.Job_Type = addNewJobPlan.Job_Type;
                jobPlan.Vessel = addNewJobPlan.Vessel;
                jobPlan.PlanHorizon = addNewJobPlan.PlanHorizon;
                jobPlan.PreviousReading = addNewJobPlan.PreviousReading;

                await _jobPlanService.InsertJobplan(jobPlan);
            }

            catch (Exception ex)
            {
                var jobPlan = new Jobplan();

                jobPlan.EquipmentName = addNewJobPlan.EquipmentName;
                jobPlan.EquipmentCode = addNewJobPlan.EquipmentCode;
                jobPlan.JobOrder = addNewJobPlan.JobOrder;
                jobPlan.CalFrequency = addNewJobPlan.CalFrequency;
                jobPlan.RunFrequency = addNewJobPlan.RunFrequency;
                jobPlan.FrequencyType = addNewJobPlan.FrequencyType;
                jobPlan.JobDescription = addNewJobPlan.JobDescription;
                jobPlan.JobStatus = addNewJobPlan.JobStatus;
                jobPlan.JobTitle = addNewJobPlan.JobTitle;
                jobPlan.LAST_DONE_DATE = addNewJobPlan.LAST_DONE_DATE;
                jobPlan.Maintenance_Type = addNewJobPlan.Maintenance_Type;
                jobPlan.NEXT_DUE_DATE = addNewJobPlan.NEXT_DUE_DATE;
                jobPlan.Priority = addNewJobPlan.Priority;
                jobPlan.Rank = addNewJobPlan.Rank;
                jobPlan.Department = addNewJobPlan.Department;
                jobPlan.AssignedTo = addNewJobPlan.AssignedTo;
                jobPlan.Job_Type = addNewJobPlan.Job_Type;
                jobPlan.Vessel = addNewJobPlan.Vessel;
                jobPlan.PlanHorizon = addNewJobPlan.PlanHorizon;
                jobPlan.PreviousReading = addNewJobPlan.PreviousReading;

                await _jobPlanService.InsertJobplan(jobPlan);

            }
        }
    }
}
