using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.Domain;

namespace RoofTop.Core.DomainServices
{
    public interface ICityService
    {
        IQueryable<City> GetAll();
    }
}
