using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using RoofTop.Core.ApplicationServices;
using RoofTop.Infrastructure.BLL.ApplicationServices;
using RoofTop.Web.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using System.IO;



namespace RoofTop.Web.Controllers
{
    [Authorize]
    public class RealEstateAdController : Controller
    {
        private readonly IRealEstateAdService _adService;
        private readonly ICityService _cityService;
        private readonly IRealEstateImageService _imgService;
        private readonly IFileService _fileService;
        private readonly HttpServerUtilityBase _server;

        public RealEstateAdController(
            IRealEstateAdService adService, 
            ICityService cityService,
            IRealEstateImageService imgService,
            IFileService fileService,
            HttpServerUtilityBase server)
        {
            _adService = adService;
            _cityService = cityService;
            _imgService = imgService;
            _fileService = fileService;
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
                    var userDir = _server.MapPath( string.Format("~/Content/UserFiles/{0}", userId) );
                    var fileName = _fileService.UploadFile(ad.PostedImage.InputStream, userDir, ad.PostedImage.FileName, ad.PostedImage.ContentLength);
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