using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Grand.Core.Domain.Vessel
{
    public partial class Vessel: BaseEntity
    {
       
  
        public string Vessel_name { get; set; }
        public string Vessel_type { get; set; }
        public string IMO { get; set; }
        public string Flag { get; set; }
        public string Hull_no { get; set; }
        public string Class { get; set; }
        public string Shipyard { get; set; }
        public string Main_Engine { get; set; }
        public string Auxiliary_Engine { get; set; }
        public int ActiveStatus { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string file { get; set; }

    }
}
