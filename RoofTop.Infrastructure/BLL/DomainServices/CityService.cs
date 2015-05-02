using System;
using System.Collections.Generic;
using System.Linq;
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

        public City GetById(int id)
        {
            return _db.Cities.Where(c => c.Id == id).FirstOrDefault();
        }

        public int Add(City ad)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Attach(City ad)
        {
            throw new NotImplementedException();
        }
    }
}
