using Grand.Core;
using Grand.Core.Domain.UnplannedJobs;
using Grand.Web.Areas.Maintenance.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Interfaces
{
    public interface ISparepartViewModelService
    {
        Task PrepareSparepartModel(SparepartModel model1, object p, bool v);
      
    }
   
}
