using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.FrequencyEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Frequency;
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
    public partial class FrequencyViewModelService : IFrequencyViewModelService
    {
        private readonly IFrequencyService _frequencyService;
        private readonly IRepository<Frequency> _FrequencyRepository;

        public FrequencyViewModelService(IFrequencyService _frequencyService,
            IRepository<Frequency> _FrequencyRepository)
        {
            this._frequencyService = _frequencyService;
            this._FrequencyRepository = _FrequencyRepository;

        }
        Task<IPagedList<Frequency>> IFrequencyViewModelService.GetAllFrequencies(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Frequency>> IFrequencyViewModelService.GetAllFrequencyAsList(string id)
        {
            await Task.FromResult(0);

            var query = _FrequencyRepository.Table;

           var result=await PagedList<Frequency>.Create(query, 0,15);

            return result;
        }
        async Task IFrequencyViewModelService.PrepareFrequencyModel(FrequencyModel addNewFrequency, object p, bool v)
        {
            try
            {

                var frequency = new Frequency();

                frequency.Frequencies = addNewFrequency.Frequencies;
               
                await _frequencyService.InsertFrequency(frequency);
            }
            catch (Exception ex)
            {
                var frequency = new Frequency();

                frequency.Frequencies = addNewFrequency.Frequencies;

                await _frequencyService.InsertFrequency(frequency);
            }
        }

    }
}
