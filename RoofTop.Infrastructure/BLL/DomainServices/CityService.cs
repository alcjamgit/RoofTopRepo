using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.DomainServices;
using RoofTop.Core.Entities;

namespace RoofTop.Infrastructure.BLL.DomainServices
{
    public class CityService: ICityService
    {
        private IApplicationDbContext _db;

        public CityService(IApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<City> GetAll()
        {
            return _db.Cities.AsQueryable();
        }

        public IQueryable<City> Find(Expression<Func<City, bool>> predicate)
        {
            return _db.Cities.Where(predicate);
        }

        public virtual City Add(City ad)
        {
            return _db.Cities.Add(ad);
        }


        public City Delete(City entity)
        {
            return _db.Cities.Remove(entity);
        }

        public void Edit(City entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
