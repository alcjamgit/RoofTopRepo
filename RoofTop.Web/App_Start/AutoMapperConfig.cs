using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoofTop.Core.Entities;
using RoofTop.Web.Models;

namespace RoofTop.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.CreateMap<CreateAdViewModel, RealEstateAd>();
            Mapper.CreateMap<RealEstateAd, DetailsAdViewModel>();
            
        }
    }
}
