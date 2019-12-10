using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Models.Catalog
{
    public class ProductAddItemViewModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Categories { get; set; }
        public List<string> AvailableCategories { get; set; }

        public string VendorId { get; set; }

        public IList<SelectListItem> AvailableVendors { get; set; }

    }
}
