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
        private readonly IMappingService _mappingSvc;
        private readonly IRealEstateAdService _adSvc;
        private readonly ICityService _citySvc;
        private readonly IRealEstateImageService _imgSvc;
        private readonly IFileService _fileSvc;
        private readonly ICurrentUserService _curUserSvc;
        private readonly HttpServerUtilityBase _server;

        public RealEstateAdController(
            IRealEstateAdService adService, 
            ICityService cityService,
            IRealEstateImageService imgService,
            IFileService fileService,
            ICurrentUserService curUserService,
            IMappingService mappingService,
            HttpServerUtilityBase server)
        {
            _adSvc = adService;
            _citySvc = cityService;
            _imgSvc = imgService;
            _fileSvc = fileService;
            _curUserSvc = curUserService;
            _mappingSvc = mappingService;
            _server = server;
        }

        public ActionResult Index(Guid userId)
        {
            var model = _adSvc.Find(a => a.CreatedBy == userId.ToString());
            return View(model);
        }

        public ActionResult Create() 
        {
            var adModel = new CreateAdViewModel();
            adModel.Cities = new SelectList(_citySvc.GetAll(), "Id", "Name");
            return View(adModel);
        }

        [HttpPost]
        public ActionResult Create(CreateAdViewModel ad)
        {   
            if (ModelState.IsValid)
            {
                RealEstateAd realtyAd = _mappingSvc.Map<CreateAdViewModel, RealEstateAd>(ad);
                realtyAd.Id = Guid.NewGuid();
                 _adSvc.Add(realtyAd);

                if (ad.PostedImage != null)
                {
                    var userDir = _server.MapPath( string.Format("~/Content/UserFiles/{0}", _curUserSvc.UserID) );
                    var fileName = _fileSvc.UploadFile(ad.PostedImage.InputStream, userDir, ad.PostedImage.FileName, ad.PostedImage.ContentLength);
                    var img = new Image { RealEstateAd_Id = realtyAd.Id, FileName = Path.GetFileName(fileName) };
                    _imgSvc.Add(img);        
                }
                //no need to commit _imgService since DbContext is global per request using IOC lifetime manager
                _adSvc.Commit();
            }
            else
            {
                ad.Cities = new SelectList(_citySvc.GetAll(), "Id", "Name");
                return View(ad);
            }

            return RedirectToAction("Index", new { userId =_curUserSvc.UserID } );
        }

        public ActionResult Details(Guid id)
        {
            var adViewModel = new DetailsAdViewModel();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}