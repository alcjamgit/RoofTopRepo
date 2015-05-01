using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using RoofTop.Web.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace RoofTop.Web.Controllers
{
    [Authorize]
    public class RealEstateAdController : Controller
    {
        private readonly IRealEstateAdService _adService;
        private readonly ICityService _cityService;

        public RealEstateAdController(IRealEstateAdService adService, ICityService cityService)
        {
            _adService = adService;
            _cityService = cityService;
        }

        public ActionResult Index()
        {
            var model = _adService.GetAll();
            return View(model);
        }
        public ActionResult Create() 
        {
            var adModel = new CreateAdViewModel();
            adModel.Cities = new SelectList(_cityService.GetAll(), "Id", "Name");
            return View(adModel);
        }

        [HttpPost]
        public ActionResult Create(CreateAdViewModel ad)
        {
            if (ModelState.IsValid)
            {
                RealEstateAd realtyAd = Mapper.Map<CreateAdViewModel, RealEstateAd>(ad);
                realtyAd.CreatedBy = User.Identity.GetUserName();
                _adService.Add(realtyAd);
            }
            return RedirectToAction("");
        }

        public ActionResult Details(Guid id)
        {
            var adViewModel = new DetailsAdViewModel();
            return View();
        }
    }
}