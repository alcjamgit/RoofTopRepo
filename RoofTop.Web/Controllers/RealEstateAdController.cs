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
using RoofTop.Infrastructure.BLL.ApplicationServices;


namespace RoofTop.Web.Controllers
{
    [Authorize]
    public class RealEstateAdController : Controller
    {
        private readonly IRealEstateAdService _adService;
        private readonly ICityService _cityService;
        private readonly IRealEstateImageService _imgService;
        private readonly HttpServerUtilityBase _server;

        public RealEstateAdController(
            IRealEstateAdService adService, 
            ICityService cityService,
            IRealEstateImageService imgService,
            HttpServerUtilityBase server)
        {
            _adService = adService;
            _cityService = cityService;
            _imgService = imgService;
            _server = server;
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
            var userId = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                RealEstateAd realtyAd = Mapper.Map<CreateAdViewModel, RealEstateAd>(ad);
                realtyAd.CreatedBy = userId;
                _adService.Add(realtyAd);

                if (ad.PostedImage != null)
                {
                    var fileService = new UserFileService(ad.PostedImage, _server, userId);
                    var fileName = fileService.UploadImage();
                    //var fileName = fileService.UploadImage(ad.PostedImage.InputStream, 
                    //    ad.PostedImage.FileName, 
                    //    ad.PostedImage.ContentLength );
                    var img = new Image { RealEstateAd_Id = realtyAd.Id};
                    _imgService.Add(img);
                }

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