using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class JobMasterModel : BaseGrandEntityModel
    {
        public string JobCode { get; set; }
        public string JobType { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
    }

}


