using Grand.Core;
using Grand.Core.Domain.MakerEntity;
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
    public partial class MakerViewModelService : IMakerViewModelService
    {
        private readonly IMakerService _makerService;
        public MakerViewModelService(IMakerService _makerService)
        {
            this._makerService = _makerService;
            
        }
        Task<IPagedList<Maker>> IMakerViewModelService.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<Maker>> IMakerViewModelService.GetAllMakersAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IMakerViewModelService.PrepareMakerModel(MakerModel addNewMaker, object p, bool v)
        {
            try
            {

                var maker = new Maker();

                maker.Name = addNewMaker.Name;
                maker.Code = addNewMaker.Code;
                maker.Country = addNewMaker.Country;
                
                await  _makerService.InsertMaker(maker);
            }
            catch (Exception ex)
            {
                var maker = new Maker();
                maker.Name = addNewMaker.Name;
                maker.Code = addNewMaker.Code;
                maker.Country = addNewMaker.Country;

                await _makerService.InsertMaker(maker);

            }
        }
    }
}
