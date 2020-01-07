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
    public partial class MakerViewModelService1 : IMakerViewModelService1
    {
        private readonly IMakerService1 _makerService;
        public MakerViewModelService1(IMakerService1 _makerService)
        {
            this._makerService = _makerService;
        }
        Task<IPagedList<Maker1>> IMakerViewModelService1.GetAllMakers(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }
        Task<IPagedList<Maker1>> IMakerViewModelService1.GetAllMakersAsList(string id)
        {
            throw new NotImplementedException();
        }

        async Task IMakerViewModelService1.PrepareMakerModel(MakerModel1 addNewMaker, object p, bool v)
        {
            try
            {

                var maker = new Maker1();

                maker.Maker = addNewMaker.Maker;
                maker.Model = addNewMaker.Model;
                maker.Remark = addNewMaker.Remark;
              
                await  _makerService.InsertMaker(maker);
                // var vessel = addNewVessel.ToEntity();

            }
            catch (Exception )
            {
                var maker = new Maker1();
                maker.Maker = addNewMaker.Maker;
                maker.Model = addNewMaker.Model;
                maker.Remark = addNewMaker.Remark;

                await _makerService.InsertMaker(maker);

            }
        }
    }
}
