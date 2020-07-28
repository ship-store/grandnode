using Grand.Core;
using Grand.Core.Domain.BreakdownJob;
using Grand.Core.Domain.Sparepart;
using Grand.Services.BreakdownJob;
using Grand.Services.Spareparts;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class SparepartViewModelService : ISparepartViewModelService
    {
        private readonly ISparepartService _sparepartService;
        public SparepartViewModelService(ISparepartService _sparepartService)
        {
            this._sparepartService = _sparepartService;
        }
       
        async Task ISparepartViewModelService.PrepareSparepartModel(SparepartModel addNewSparepart, object p, bool v)
        {
            try
            {
                var sparepart = new Sparepart();

                sparepart.EquipmentCode= addNewSparepart.EquipmentCode;

                sparepart.EquipmentName = addNewSparepart.EquipmentName;
                sparepart.SPAR_PARTS_DESCRIPTION = addNewSparepart.SPAR_PARTS_DESCRIPTION;
                sparepart.PART_NUMBER = addNewSparepart.PART_NUMBER;
                sparepart.DRAWING_NO = addNewSparepart.DRAWING_NO;
                sparepart.SPECIFICATION = addNewSparepart.SPECIFICATION;
                sparepart.POSITION_NUMBER = addNewSparepart.POSITION_NUMBER;
                sparepart.Vessel = addNewSparepart.Vessel;
                sparepart.Criticals = addNewSparepart.Criticals;
                await _sparepartService.InsertSparepart(sparepart);
            }
            catch (Exception ex)
            {

                var sparepart = new Sparepart();
                sparepart.EquipmentCode = addNewSparepart.EquipmentCode;

                sparepart.EquipmentName = addNewSparepart.EquipmentName;
                sparepart.SPAR_PARTS_DESCRIPTION = addNewSparepart.SPAR_PARTS_DESCRIPTION;
                sparepart.PART_NUMBER = addNewSparepart.PART_NUMBER;
                sparepart.DRAWING_NO = addNewSparepart.DRAWING_NO;
                sparepart.SPECIFICATION = addNewSparepart.SPECIFICATION;
                sparepart.POSITION_NUMBER = addNewSparepart.POSITION_NUMBER;
                sparepart.Vessel = addNewSparepart.Vessel;
                sparepart.Criticals = addNewSparepart.Criticals;
                await _sparepartService.InsertSparepart(sparepart);
            }
        }
    }
}
