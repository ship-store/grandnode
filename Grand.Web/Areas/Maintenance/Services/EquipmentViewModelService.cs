using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grand.Services.Equipments;
using Grand.Core.Domain.Equipment;
using Grand.Core;

namespace Grand.Web.Areas.Maintenance.Services
{
    public class EquipmentViewModelService: IEquipmentViewModelService
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentViewModelService(IEquipmentService _equipmentService)
        {
            this._equipmentService = _equipmentService;
        }

        public Task PrepareEquipmentModel(EquipmentView equipmentview, string v1, bool v2)
        {
            throw new NotImplementedException();
        }

        async Task IEquipmentViewModelService.PrepareEquipmentModel(EquipmentModel equipmentModel, string v1, bool v2)
        {
            try
            {
                var equipment = new Equipment();
                equipment.Sub1_number = equipmentModel.Sub1_number;
                equipment.Sub1_description = equipmentModel.Sub1_description;
                equipment.Sub2_number = equipmentModel.Sub2_number;
                equipment.Sub2_description = equipmentModel.Sub2_description;
                equipment.Sub3_number = equipmentModel.Sub3_number;
                equipment.Sub3_description = equipmentModel.Sub3_description;
                equipment.Sub4_number = equipmentModel.Sub4_number;
                equipment.Sub4_description = equipmentModel.Sub4_description;
                equipment.Sub5_number = equipmentModel.Sub5_number;
                equipment.Sub5_description = equipmentModel.Sub5_description;
                equipment.Safety_level = equipmentModel.Safety_level;
                equipment.Maker = equipmentModel.Maker;
                equipment.Model = equipmentModel.Model;
                equipment.Equipment_type = equipmentModel.Equipment_type;
                equipment.Drawing_no = equipmentModel.Drawing_no;
                equipment.Department = equipmentModel.Department;
                equipment.Location = equipmentModel.Location;
                equipment.Equipment_Status = equipmentModel.Equipment_Status;
                equipment.Remark = equipmentModel.Remark;
                equipment.Vessel = equipmentModel.Vessel;
                equipment.Type = equipmentModel.Type;

            }
            catch (Exception ex)
            {
                var equipment = new Equipment();
                equipment.Sub1_number = equipmentModel.Sub1_number;
                equipment.Sub1_description = equipmentModel.Sub1_description;
                equipment.Sub2_number = equipmentModel.Sub2_number;
                equipment.Sub2_description = equipmentModel.Sub2_description;
                equipment.Sub3_number = equipmentModel.Sub3_number;
                equipment.Sub3_description = equipmentModel.Sub3_description;
                equipment.Sub4_number = equipmentModel.Sub4_number;
                equipment.Sub4_description = equipmentModel.Sub4_description;
                equipment.Sub5_number = equipmentModel.Sub5_number;
                equipment.Sub5_description = equipmentModel.Sub5_description;
                equipment.Safety_level = equipmentModel.Safety_level;
                equipment.Maker = equipmentModel.Maker;
                equipment.Model = equipmentModel.Model;
                equipment.Equipment_type = equipmentModel.Equipment_type;
                equipment.Drawing_no = equipmentModel.Drawing_no;
                equipment.Department = equipmentModel.Department;
                equipment.Location = equipmentModel.Location;
                equipment.Equipment_Status = equipmentModel.Equipment_Status;
                equipment.Remark = equipmentModel.Remark;
                equipment.Vessel = equipmentModel.Vessel;
                equipment.Type = equipmentModel.Type;
                await _equipmentService.InsertEquipment(equipment);
            }
        }

        Task IEquipmentViewModelService.PrepareEquipmentModel(EquipmentModel equipmentModel, object p, bool v)
        {
            throw new NotImplementedException();
        }

        //async Task IEquipmentViewModelService.PrepareEquipmentModel(EquipmentModel equipmentModel, object p, bool v)
        //{
        //    try
        //    {
        //        var equipment = new Equipment();
        //        equipment.Sub1_number = equipmentModel.Sub1_number;
        //        equipment.Sub1_description = equipmentModel.Sub1_description;
        //        equipment.Sub2_number = equipmentModel.Sub2_number;
        //        equipment.Sub2_description = equipmentModel.Sub2_description;
        //        equipment.Sub3_number = equipmentModel.Sub3_number;
        //        equipment.Sub3_description = equipmentModel.Sub3_description;
        //        equipment.Sub4_number = equipmentModel.Sub4_number;
        //        equipment.Sub4_description = equipmentModel.Sub4_description;
        //        equipment.Sub5_number = equipmentModel.Sub5_number;
        //        equipment.Sub5_description = equipmentModel.Sub5_description;
        //        equipment.Safety_level = equipmentModel.Safety_level;
        //        equipment.Maker = equipmentModel.Maker;
        //        equipment.Model = equipmentModel.Model;
        //        equipment.Equipment_type = equipmentModel.Equipment_type;
        //        equipment.Drawing_no = equipmentModel.Drawing_no;
        //        equipment.Department = equipmentModel.Department;
        //        equipment.Location = equipmentModel.Location;
        //        equipment.Equipment_Status = equipmentModel.Equipment_Status;
        //        equipment.Remark = equipmentModel.Remark;
        //        equipment.Vessel = equipmentModel.Vessel;

        //    }
        //    catch (Exception ex)
        //    {
        //        var equipment = new Equipment();
        //        equipment.Sub1_number = equipmentModel.Sub1_number;
        //        equipment.Sub1_description = equipmentModel.Sub1_description;
        //        equipment.Sub2_number = equipmentModel.Sub2_number;
        //        equipment.Sub2_description = equipmentModel.Sub2_description;
        //        equipment.Sub3_number = equipmentModel.Sub3_number;
        //        equipment.Sub3_description = equipmentModel.Sub3_description;
        //        equipment.Sub4_number = equipmentModel.Sub4_number;
        //        equipment.Sub4_description = equipmentModel.Sub4_description;
        //        equipment.Sub5_number = equipmentModel.Sub5_number;
        //        equipment.Sub5_description = equipmentModel.Sub5_description;
        //        equipment.Safety_level = equipmentModel.Safety_level;
        //        equipment.Maker = equipmentModel.Maker;
        //        equipment.Model = equipmentModel.Model;
        //        equipment.Equipment_type = equipmentModel.Equipment_type;
        //        equipment.Drawing_no = equipmentModel.Drawing_no;
        //        equipment.Department = equipmentModel.Department;
        //        equipment.Location = equipmentModel.Location;
        //        equipment.Equipment_Status = equipmentModel.Equipment_Status;
        //        equipment.Remark = equipmentModel.Remark;
        //        equipment.Vessel = equipmentModel.Vessel;

        //        await _equipmentService.InsertEquipment(equipment);
        //    }
        //}
        Task<IPagedList<Equipment>> IEquipmentViewModelService.GetAllEquipment(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

    }
}
