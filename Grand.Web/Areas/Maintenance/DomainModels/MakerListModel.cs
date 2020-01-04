﻿using Grand.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Framework.Mvc.ModelBinding;


namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class MakerListModel : BaseGrandModel
    {
        [GrandResourceDisplayName("Maintenance.Maker.List.SearchName")]

        public string SearchName { get; set; }
    }
}
