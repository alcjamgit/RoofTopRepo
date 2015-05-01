using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;

namespace RoofTop.Infrastructure.BLL
{
    public class RealtEstateAdService : IRealEstateAdService
    {
        private IApplicationDbContext _db;
        public RealtEstateAdService(IApplicationDbContext db)
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

        public int Add(RealEstateAd ad)
        {
            //just a mockup
            var id = Guid.NewGuid();

            ad.Id = id;
            ad.Created = DateTime.Now;
            ad.Modified = DateTime.Now;
            ad.City_Id = 1;

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
