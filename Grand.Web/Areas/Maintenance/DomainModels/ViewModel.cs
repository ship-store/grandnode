using Grand.Core.Domain.Equipment;
using Grand.Core.Domain.Jobplan;
using Grand.Core.Domain.Sparepart;
using System;
using System.Collections.Generic;
using System.Linq;
using Grand.Core.Domain.JobType;

using System.Threading.Tasks;
using Grand.Core.Domain.CbmEntity;

namespace Grand.Web.Areas.Maintenance.DomainModels
{
    public class ViewModel
    {
        public Equipment SelectedEquipment { get; set; }
        public IEnumerable<Equipment> AllEquipments { get; set; }

        public IEnumerable <Jobplan> SelectedJobPlan { get; set; }
        public Jobplan SelectedEquipmentView { get; set; }
        public IEnumerable <Sparepart> SelectedSparepart { get; set; }
        public string VName { get; set; }
        public IEnumerable<JobplanListModel> SelectedJobPlanView { get; set; }
        public IEnumerable<JobType> JobTypeList { get; set; }

        //public Grand.Core.Domain.EquipmentTypeEntity.EquipmentType equipmentTypeList { get; set; }
        public List<Grand.Core.Domain.EquipmentTypeEntity.EquipmentType> equipmentTypeList { get; set; }

        //public Grand.Core.Domain.CbmEntity.CBM cbmList { get; set; }
        public IEnumerable<Grand.Core.Domain.CbmEntity.CBM> cbmList { get; set; }
        public List<string> EquipmentTypeList { get; set; }

    }
}
