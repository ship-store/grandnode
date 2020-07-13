using Grand.Core;
using Grand.Core.Data;
using Grand.Core.Domain.MakerEntity;
using Grand.Services.Vessel;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Grand.Core.Domain.LocationEntity.Location> _locationRepository;
        
        public LocationService(IRepository<Grand.Core.Domain.LocationEntity.Location> _locationRepository)
        {
            this._locationRepository = _locationRepository;
        }
       
        async Task<IPagedList<Core.Domain.LocationEntity.Location>> ILocationService.GetAllLocations(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _locationRepository.Table;
         
            return await PagedList< Grand.Core.Domain.LocationEntity.Location>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.LocationEntity.Location>> ILocationService.GetAllLocationAsList()
        {
            var query = _locationRepository.Table;


           

            return await PagedList<Grand.Core.Domain.LocationEntity.Location>.Create(query ,0,15);
        }

        Task ILocationService.PrepareLocationModel(Core.Domain.LocationEntity.Location model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertLocation(Core.Domain.LocationEntity.Location location)
        {


            await _locationRepository.InsertAsync(location);


        }
        public virtual Task<Core.Domain.LocationEntity.Location> GetLocationById(string locationId)
        {
            return _locationRepository.GetByIdAsync(locationId);
        }
        public virtual async Task UpdateLocation(Core.Domain.LocationEntity.Location location)
        {
            await _locationRepository.UpdateAsync(location);
        }
    }
}
