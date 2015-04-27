using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.DomainServices;

namespace RoofTop.Infrastructure.BLL.DomainServices
{
    public class CityService: ICityService
    {
        private IApplicationDbContext _db;
        public CityService(IApplicationDbContext db)
        {
            _db = db;
        }
        public IQueryable<Core.Domain.City> GetAll()
        {
            return _db.Cities.AsQueryable();
        }
    }
}
