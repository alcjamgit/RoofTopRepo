using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.DomainServices;
using RoofTop.Core.Entities;

namespace RoofTop.Infrastructure.DAL
{
    public class RealEstateRepository : IRepository<RealEstateAd>
    {
        private DbSet<RealEstateAd> _dbSet;
        public RealEstateRepository()
        {
            _dbSet = new IDbSet<RealEstateAd>();
        }

        public IQueryable<RealEstateAd> GetQuery<RealEstateAd>
            (Expression<Func<RealEstateAd, bool>> predicate) where RealEstateAd : class
        {
            return _dbSet.Where<RealEstateAd>(predicate).AsQueryable();
        }
    }
}
