using Grand.Framework.Mvc.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Framework.Mvc.ModelBinding;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class VesselModel : BaseGrandEntityModel
    {

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Vessel_name")]
        public string Vessel_name { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Vessel_type")]
        public string Vessel_type { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.IMO")]
        public string IMO { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Flag")]
        public string Flag { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Hull_no")]
        public string Hull_no { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Class")]
        public string Class { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Shipyard")]
        public string Shipyard { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Main_Engine")]
        public string Main_Engine { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Auxiliary_Engine")]
        public string Auxiliary_Engine { get; set; }
        public int ActiveStatus { get; set; }

    }
    public class VesselForDisplay : BaseGrandEntityModel
    {


        public string VesselID { get; set; }
        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Vessel_name")]
        public string Vessel_name { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Vessel_type")]
        public string Vessel_type { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.IMO")]
        public string IMO { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Flag")]
        public string Flag { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Hull_no")]
        public string Hull_no { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Class")]
        public string Class { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Shipyard")]
        public string Shipyard { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Main_Engine")]
        public string Main_Engine { get; set; }

        [GrandResourceDisplayName("Maintenance.Vessel.Fields.Auxiliary_Engine")]
        public string Auxiliary_Engine { get; set; }
        public int ActiveStatus { get; set; }
    }
    
}
