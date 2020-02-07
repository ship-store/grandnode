﻿using Grand.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class JobplanListModel
    {
        [GrandResourceDisplayName("Maintenance.Vessel.List.SearchName")]
        public string SearchName { get; set; }
    }
}
