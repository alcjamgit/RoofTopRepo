using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Entities;

namespace RoofTop.Core.DomainServices
{
    public interface IRealEstateAdService
    {
        RealEstateAd GetById(Guid id);
        IQueryable<RealEstateAd> GetAll();
        int Add(RealEstateAd ad);
        bool Delete(Guid id);
        bool Attach(RealEstateAd ad);
    }
}
