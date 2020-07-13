using Grand.Core;
using Grand.Core.Domain.Vendors;
using Grand.Core.Domain.MakerEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grand.Services.Location
{
    public interface ILocationService
    {
         Task<IPagedList<Grand.Core.Domain.LocationEntity.Location>> GetAllLocations(string name = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

         Task<IList<Grand.Core.Domain.LocationEntity.Location>> GetAllLocationAsList();
           Task PrepareLocationModel(Grand.Core.Domain.LocationEntity.Location model1, object p, bool v);
       
        Task InsertLocation(Core.Domain.LocationEntity.Location location);
        Task<Core.Domain.LocationEntity.Location> GetLocationById(string Id);
        Task UpdateLocation(Core.Domain.LocationEntity.Location location);
    }
}
