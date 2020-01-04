
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Equipments
{
    public interface IEquipmentService
    {
        Task InsertEquipment(Grand.Core.Domain.Equipment.Equipment equipment);
    }
}
