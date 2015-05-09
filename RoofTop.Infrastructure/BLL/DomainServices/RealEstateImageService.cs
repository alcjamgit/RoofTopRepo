using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using System.Linq.Expressions;

namespace RoofTop.Infrastructure.BLL.DomainServices
{
    public class RealEstateImageService : IRealEstateImageService
    {
        private IApplicationDbContext _db;

        public RealEstateImageService(IApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Image> GetAll()
        {
            return _db.Images.AsQueryable();
        }

        public IQueryable<Image> Find(Expression<Func<Image, bool>> predicate)
        {
            return _db.Images.Where(predicate);
        }

        public virtual Image Add(Image ad)
        {
            return _db.Images.Add(ad);
        }


        public Image Delete(Image entity)
        {
            return _db.Images.Remove(entity);
        }

        public void Edit(Image entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
