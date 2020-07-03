using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.FrequencyTypeEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Frequency;
using Grand.Services.FrequencyType;
using Grand.Services.Maker;
using Grand.Services.Vessel;
using Grand.Web.Areas.Maintenance.DomainModels;
using Grand.Web.Areas.Maintenance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Web.Areas.Maintenance.Services
{
    public partial class FrequencyTypeViewModelService : IFrequencyTypeViewModelService
    {
        private readonly IFrequencyTypeService _frequencyTypeService;
        private readonly IRepository<FrequencyType> _FrequencyTypeRepository;

        public FrequencyTypeViewModelService(IFrequencyTypeService _frequencyTypeService,
            IRepository<FrequencyType> _FrequencyTypeRepository)
        {
            this._frequencyTypeService = _frequencyTypeService;
            this._FrequencyTypeRepository = _FrequencyTypeRepository;

        }
        Task<IPagedList<FrequencyType>> IFrequencyTypeViewModelService.GetAllFrequencyTypes(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<FrequencyType>> IFrequencyTypeViewModelService.GetAllFrequencyTypeAsList(string id)
        {
            await Task.FromResult(0);

            var query = _FrequencyTypeRepository.Table;

           var result=await PagedList<FrequencyType>.Create(query, 0,15);

            return result;
        }
        async Task IFrequencyTypeViewModelService.PrepareFrequencyTypeModel(FrequencyTypeModel addNewFrequencyType, object p, bool v)
        {
            try
            {

                var frequencyType = new FrequencyType();

                frequencyType.Frequency_type = addNewFrequencyType.Frequency_type;
               
                await _frequencyTypeService.InsertFrequencyType(frequencyType);
            }
            catch (Exception ex)
            {
                var frequencyType = new FrequencyType();

                frequencyType.Frequency_type = addNewFrequencyType.Frequency_type;

                await _frequencyTypeService.InsertFrequencyType(frequencyType);
            }
        }

    }
}
