using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using RoofTop.Infrastructure.BLL.ApplicationServices;

namespace RoofTop.Infrastructure.BLL.DomainServices
{
    public class RealEstateAdService : IRealEstateAdService
    {
        private IApplicationDbContext _db;
        
        public RealEstateAdService(IApplicationDbContext db)
        {
            _db = db;
        }
        public RealEstateAd GetById(Guid id)
        {
            return _db.RealEstateAds.Where(r => r.Id == id).FirstOrDefault();
        }


        public IQueryable<RealEstateAd> GetAll()
        {
            return _db.RealEstateAds.AsQueryable();
        }

        public virtual int Add(RealEstateAd ad)
        {
            _db.RealEstateAds.Add(ad);
            return _db.SaveChanges();
            
        }

        public bool Delete(Guid id)
        {
 	        throw new NotImplementedException();
        }

        public bool Attach(RealEstateAd ad)
        {
 	        throw new NotImplementedException();
        }

    }
}
