using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.DepartmentEntity;
using Grand.Core.Domain.EquipmentTypeEntity;
using Grand.Core.Domain.LocationEntity;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Department;
using Grand.Services.EquipmentType;
using Grand.Services.Location;
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
    public partial class LocationViewModelService : ILocationViewModelService
    {
        private readonly ILocationService _locationService;
        private readonly IRepository<Location> _LocationRepository;

        public LocationViewModelService(ILocationService _locationService,
            IRepository<Location> _LocationRepository)
        {
            this._locationService = _locationService;
            this._LocationRepository = _LocationRepository;

        }
        Task<IPagedList<Location>> ILocationViewModelService.GetAllLocations(string name, int pageIndex, int pageSize, bool showHidden)
        {
            throw new NotImplementedException();
        }

        async Task<IPagedList<Location>> ILocationViewModelService.GetAllLocationAsList(string id)
        {
            await Task.FromResult(0);

            var query = _LocationRepository.Table;

           var result=await PagedList<Location>.Create(query, 0,15);

            return result;
        }
        async Task ILocationViewModelService.PrepareLocationModel(LocationModel addNewLocation, object p, bool v)
        {
            try
            {

                var location = new Location();

                location.Locations = addNewLocation.Locations;
               
                await  _locationService.InsertLocation(location);
            }
            catch (Exception ex)
            {
                var location = new Location();

                location.Locations = addNewLocation.Locations;

                await _locationService.InsertLocation(location);

            }
        }

     
    }
}
