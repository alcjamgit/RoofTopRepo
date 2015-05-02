using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeDbSet;
using RoofTop.Core.DomainServices;
using RoofTop.Core.Entities;

namespace RoofTop.Test.Fakes
{
    public class FakeDbContext: IApplicationDbContext
    {
        public FakeDbContext()
        {
            this.RealEstateAds = new InMemoryDbSet<RealEstateAd>();
            this.Cities = new InMemoryDbSet<City> { 
                new City { Id = 1, Name = "Makati", Region = "NCR" },
                new City { Id = 2, Name = "Taguig", Region = "NCR" },
            };
            this.Images = new InMemoryDbSet<Image>();
        }
        public IDbSet<RealEstateAd> RealEstateAds { get; set; }
        public IDbSet<City> Cities { get; set; }
        public IDbSet<Image> Images { get; set; }
        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
            changes += DbSetHelper.IncrementPrimaryKey<RealEstateAd>(x => x.Id, this.RealEstateAds);

            return changes;
        }

        public void Dispose()
        {
            
        }
    }
}
