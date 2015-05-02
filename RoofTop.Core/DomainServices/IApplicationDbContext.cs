using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;


namespace RoofTop.Core.DomainServices
{
    public interface IApplicationDbContext: IDisposable
    {
        IDbSet<RealEstateAd> RealEstateAds { get; set; }
        IDbSet<City> Cities { get; set; }
        IDbSet<Image> Images { get; set; }
        int SaveChanges();
    }
}
