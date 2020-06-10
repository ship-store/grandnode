using Grand.Core.Domain.Jobplan;
using Grand.Services.Jobplan;
using Grand.Services.Vessel;
using Grand.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class DashBoardController : BaseAdminController
    {
        private readonly IJobplanService _jobplanService;
        private readonly IVesselService _vesselService;
        public DashBoardController(IJobplanService _jobplanService,
              IVesselService _vesselService)
        {
            this._jobplanService = _jobplanService;
            this._vesselService = _vesselService;
        }
       
        
        public async Task<IActionResult> Index()
        {
            var Vessels = await _vesselService.GetAllVessels("", 0, 500, true);
            var TotalVessel=Vessels.Count();

             var VesselName = HttpContext.Session.GetString("VesselName").ToString();
            var Jobplanlist = await _jobplanService.GetAllJobplans("", 0, 500);
            List<Jobplan> jobplanlist = new List<Jobplan>();

            foreach (Jobplan item in Jobplanlist.Where(x => x.Vessel.ToLower() == VesselName.ToLower() && x.NEXT_DUE_DATE != null).OrderBy(x => x.NEXT_DUE_DATE).ToList())
            {
                var date = Convert.ToDateTime(item.NEXT_DUE_DATE);



                var UperLimit = date.AddDays(10);
                var temp = DateTime.Now.AddDays(10);
                var dueRhs = (item.DueRhs / 100) * 10;
                var duerhs1 = item.DueRhs - dueRhs;
                var cureentrhs = item.LastReading;


                if (temp >= date)
                {
                    jobplanlist.Add(item);
                }

                if (cureentrhs <= duerhs1)
                {
                    jobplanlist.Add(item);
                }
            }
            var p = jobplanlist.OrderBy(x => x.NEXT_DUE_DATE).ToList();


            //for Dashbord values
            var Data = jobplanlist.ToList().Where(x => x.JobStatus == 0);
            var TotalDueJobs = Data.Count();

            DateTime dateTime = DateTime.UtcNow.Date;
            var Today = dateTime.ToString("yyyy-MM-dd");
            var nextWeekDay = DateTime.Today.AddDays(10).ToString("yyyy-MM-dd");
         
            var DueToday = p.Where(x=>x.NEXT_DUE_DATE==Today).ToList().Count();
          
            var DueOftheWeek = p.Where(x => Convert.ToDateTime(x.NEXT_DUE_DATE) <= Convert.ToDateTime(nextWeekDay) &&
             Convert.ToDateTime(x.NEXT_DUE_DATE) >= Convert.ToDateTime(Today)).ToList();

            var DueOfWeek = DueOftheWeek.Count();

            var DueforMonth= p.Where(x => Convert.ToDateTime(x.NEXT_DUE_DATE) <= Convert.ToDateTime(DateTime.Today.AddDays(30).ToString("yyyy-MM-dd")) &&
              Convert.ToDateTime(x.NEXT_DUE_DATE) >= Convert.ToDateTime(Today)).ToList();

            var DueofMonth = DueforMonth.Count();

            ViewBag.DueToday = DueToday;
            ViewBag.TotalVessel = TotalVessel;
            ViewBag.DueOfWeek = DueOfWeek;
            ViewBag.DueofMonth = DueofMonth;

            return View();
        }


    }
}
