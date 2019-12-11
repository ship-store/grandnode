using Grand.Framework.Mvc.ModelBinding;
using Grand.Framework.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grand.Web.Areas.Admin.Models.Orders
{
    public partial class PortwiseReportModel : BaseGrandModel
    {
        public PortwiseReportModel()
        {
            AvailableOrderStatuses = new List<SelectListItem>();
            AvailablePaymentStatuses = new List<SelectListItem>();
        }

        [GrandResourceDisplayName("Admin.Reports.Country.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [GrandResourceDisplayName("Admin.Reports.Country.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }


        [GrandResourceDisplayName("Admin.Reports.Country.OrderStatus")]
        public int OrderStatusId { get; set; }
        [GrandResourceDisplayName("Admin.Reports.Country.PaymentStatus")]
        public int PaymentStatusId { get; set; }

        public IList<SelectListItem> AvailableOrderStatuses { get; set; }
        public IList<SelectListItem> AvailablePaymentStatuses { get; set; }

        public List<PortwiseReportLineModel> PortwiseData { get; set; }
    }

    public partial class PortwiseReportLineModel : BaseGrandModel
    {
        [GrandResourceDisplayName("Admin.Reports.Country.Fields.CountryName")]
        public string PortName { get; set; }    

        [GrandResourceDisplayName("Admin.Reports.Country.Fields.TotalOrders")]
        public int TotalOrders { get; set; }

        [GrandResourceDisplayName("Admin.Reports.Country.Fields.SumOrders")]
        public long NoOfVessels { get; set; }
    }
}