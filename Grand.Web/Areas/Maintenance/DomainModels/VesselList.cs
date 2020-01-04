using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class VesselList
    {

        public int Vessel_id { get; set; }
        public string Vessel_name { get; set; }
        public string Vessel_type { get; set; }
        public string IMO { get; set; }
        public string Flag { get; set; }
        public string Hull_no { get; set; }
        public string Class { get; set; }
        public string Shipyard { get; set; }
        public string Main_Engine { get; set; }
        public string Auxiliary_Engine { get; set; }
    }
}
