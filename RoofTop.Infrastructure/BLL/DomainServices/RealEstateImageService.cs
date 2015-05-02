using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;

namespace RoofTop.Infrastructure.BLL.DomainServices
{
    public class RealEstateImageService : IRealEstateImageService
    {
        private IApplicationDbContext _db;
        public RealEstateImageService(IApplicationDbContext db)
        {
            _db = db;
        }
        public Image GetById(int id)
        {
            return _db.Images.Where(r => r.Id == id).FirstOrDefault();
        }


        public IQueryable<Image> GetAll()
        {
            return _db.Images.AsQueryable();
        }

        /// <summary>
        /// Save image info to the database
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public int Add(Image img)
        {
            //Save image
            _db.Images.Add(img);
            return _db.SaveChanges();
            
        }

        public bool Delete(int id)
        {
 	        throw new NotImplementedException();
        }

        public bool Attach(RealEstateAd ad)
        {
 	        throw new NotImplementedException();
        }


    }
}
