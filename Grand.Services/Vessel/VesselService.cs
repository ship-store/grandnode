using Grand.Core;
using Grand.Core.Caching;
using Grand.Core.Data;
using Grand.Core.Domain.Vessel;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Grand.Services.Vessel
{
    public class VesselService : IVesselService
    {
        private readonly IRepository<Grand.Core.Domain.Vessel.Vessel> _vesselRepository;
        private readonly ICacheManager _cacheManager;
        private string key;

        public VesselService(IRepository<Grand.Core.Domain.Vessel.Vessel> _vesselRepository,
            ICacheManager cacheManager)
        {
            this._vesselRepository = _vesselRepository;
            this._cacheManager = cacheManager;
        }
       
        async Task<IPagedList<Core.Domain.Vessel.Vessel>> IVesselService.GetAllVessels(string name, int pageIndex, int pageSize, bool showHidden)
        {
            var query = _vesselRepository.Table;
         
            return await PagedList< Grand.Core.Domain.Vessel.Vessel>.Create(query, pageIndex, pageSize);
        }
        
         //TODO
        // page size paramater need tobe setted
        async Task<IList<Core.Domain.Vessel.Vessel>> IVesselService.GetAllVesselAsList()
        {
            var query = _vesselRepository.Table;


           

            return await PagedList<Grand.Core.Domain.Vessel.Vessel>.Create(query ,0,15);
        }

        Task IVesselService.PrepareVesselModel(Core.Domain.Vessel.Vessel model1, object p, bool v)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertVessel(Core.Domain.Vessel.Vessel vessel)
        {


            await _vesselRepository.InsertAsync(vessel);


        }

        
    


    public async Task<IList<Core.Domain.Vessel.Vessel>> GetVesselByIds(string[] vesselIds)
        {
            if (vesselIds == null || vesselIds.Length == 0)
                return new List<Core.Domain.Vessel.Vessel>();
           
            var builder = Builders<Core.Domain.Vessel.Vessel>.Filter;

          
            var filter = builder.Where(x => vesselIds.Contains(x.Id));

              var vessels = await _vesselRepository.Collection.Find(filter).ToListAsync();

            var sortedProducts = new List<Core.Domain.Vessel.Vessel>();
            
            foreach (string id in vesselIds)
            {
                var product = vessels.Find(x => x.Id == id);
                if (product != null)
                    sortedProducts.Add(product);
            }
           
            return sortedProducts;


        }

        //public Task DeleteVessel(Core.Domain.Vessel.Vessel vessel)
        //{
        //    throw new NotImplementedException();
        //}
        public virtual async Task DeleteVessel(Core.Domain.Vessel.Vessel vessel)
        {
            if (vessel == null)
                throw new ArgumentNullException("Vessel");


            //deleted product
            await _vesselRepository.DeleteAsync(vessel);

            

        }
        public virtual async Task UpdateVessel(Core.Domain.Vessel.Vessel vessel)
        {
            await _vesselRepository.UpdateAsync(vessel);
        }
        public virtual Task<Core.Domain.Vessel.Vessel> GetVesselById(string vesselId)
        {
            return _vesselRepository.GetByIdAsync(vesselId);
        }

    }
}
