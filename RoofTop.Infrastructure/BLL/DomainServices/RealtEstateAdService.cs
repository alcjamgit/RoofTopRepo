using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IQueryable<RealEstateAd> GetAll()
        {
            return _db.RealEstateAds.AsQueryable();
        }

        public virtual IQueryable<RealEstateAd> Find(Expression<Func<RealEstateAd, bool>> predicate)
        {
            return _db.RealEstateAds.Where(predicate);
        }

        public virtual RealEstateAd Add(RealEstateAd ad)
        {
            return _db.RealEstateAds.Add(ad);

        }


        public RealEstateAd Delete(RealEstateAd entity)
        {
            return _db.RealEstateAds.Remove(entity);
        }

        public void Edit(RealEstateAd entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
