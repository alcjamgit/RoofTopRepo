using System;
using RoofTop.Core.Entities;

namespace RoofTop.Core.DomainServices
{
    public interface IRealEstateImageService
    {
        int Add(Image img);
        bool Delete(int id);
        System.Linq.IQueryable<Image> GetAll();
        Image GetById(int id);
    }
}
