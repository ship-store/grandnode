using Grand.Framework.Mvc.ModelBinding;
using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class UnplannedJobListModel : BaseGrandModel
    {
        [GrandResourceDisplayName("maintenance.unplannedjob.list.search_name")]

        public string SearchName { get; set; }
    }
}

