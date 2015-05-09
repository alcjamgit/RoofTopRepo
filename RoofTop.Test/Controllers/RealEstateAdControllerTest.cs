using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoofTop.Core.ApplicationServices;
using RoofTop.Core.DomainServices;
using RoofTop.Core.Entities;
using RoofTop.Infrastructure.BLL.ApplicationServices;
using RoofTop.Infrastructure.BLL.DomainServices;
using RoofTop.Test.Fakes;
using RoofTop.Web.Controllers;
using RoofTop.Web.Models;

namespace RoofTop.Test.Controllers
{
    [TestClass]
    public class RealEstateAdControllerTest
    {
        private Mock<RealEstateAdService> _adSvc;
        private Mock<MappingService> _mappingSvc;
        private Mock<CityService> _citySvc;
        private Mock<RealEstateImageService> _imgSvc;
        private Mock<CurrentUserService> _curUserSvc;
        private Mock<HttpPostedFileBase> _postedFile;
        private Mock<UserFileService> _fileSvc;
        private Mock<HttpServerUtilityBase> _server;

        [TestInitialize]
        public void Initialize()
        {
            _adSvc = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            _adSvc.Setup(a => a.Add(It.IsAny<RealEstateAd>())).Returns(It.IsAny<RealEstateAd>());

            _citySvc = new Mock<CityService>(It.IsAny<IApplicationDbContext>());

            _imgSvc = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            _imgSvc.Setup(i => i.Add(It.IsAny<Image>())).Returns(It.IsAny<Image>());

            _fileSvc = new Mock<UserFileService>();
            _fileSvc.Setup(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(It.IsAny<string>);

            _mappingSvc = new Mock<MappingService>();
            _mappingSvc.Setup(m => m.Map<CreateAdViewModel, RealEstateAd>(It.IsAny<CreateAdViewModel>())).Returns(new RealEstateAd());

            _server = new Mock<HttpServerUtilityBase>();
            _server.Setup(s => s.MapPath(It.IsAny<string>())).Returns(It.IsAny<string>());
            _curUserSvc = new Mock<CurrentUserService>();
            _postedFile = new Mock<HttpPostedFileBase>(); 
        }
        [TestMethod]
        public void Create_Controller_Redirects_To_Index_When_Model_Is_Valid()
        {
            //Arrange
            var controller = new RealEstateAdController(_adSvc.Object, _citySvc.Object,
                _imgSvc.Object, _fileSvc.Object, _curUserSvc.Object, _mappingSvc.Object, _server.Object);
            var ad = new CreateAdViewModel() { Title = "The New Mansion", PostedImage = _postedFile.Object };
            //Act
            var view = controller.Create(ad) as RedirectToRouteResult;

            //Assert

            _adSvc.Verify(a => a.Add(It.IsAny<RealEstateAd>()), Times.Once, "Failed to call Add method of adService");
            _fileSvc.Verify(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once, "Failed to call UploadFile");
            _mappingSvc.Verify(m => m.Map<CreateAdViewModel, RealEstateAd>(It.IsAny<CreateAdViewModel>()), Times.Once, "Failed to call mapper");
            _imgSvc.Verify(i => i.Add(It.IsAny<Image>()),Times.Once);
            Assert.IsNotNull(view, "Controller does not redirect");
        }

        [TestMethod]
        public void Create_Controller_Returns_View_To_Index_When_Model_Is_Not_Valid()
        {

            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            var fileService = new Mock<UserFileService>();
            var mappingService = new Mock<MappingService>();
            var server = new Mock<HttpServerUtilityBase>();
            var curUserSvc = new Mock<CurrentUserService>();
            
            var controller = new RealEstateAdController(adService.Object, cityService.Object,
                imgService.Object, fileService.Object, curUserSvc.Object, mappingService.Object, server.Object);

            var ad = new CreateAdViewModel() { Title = "The New Mansion" };

            //Simulate validation otherwise ModelState.IsValid will always be true
            controller.ModelState.AddModelError("Error", "Title is incorrect");
            var view = controller.Create(ad) as ViewResult;
            Assert.IsNotNull(view);
            Assert.AreEqual(view.Model, ad, "Failed");
        }
        [TestMethod]
        public void Index_Returns_View()
        {
            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            var fileService = new Mock<UserFileService>();
            var mappingService = new Mock<MappingService>();
            var server = new Mock<HttpServerUtilityBase>();
            var curUserSvc = new Mock<CurrentUserService>();

            var controller = new RealEstateAdController(adService.Object, cityService.Object,
                imgService.Object, fileService.Object, curUserSvc.Object, mappingService.Object, server.Object);
            var view = controller.Index(Guid.NewGuid()) as ViewResult;
            Assert.IsNotNull(view);
        }

        [TestMethod]
        public void Index_Returns_Correct_Number_Of_Ads_In_The_View()
        {
            var adService = new Mock<IRealEstateAdService>();
            var ads = new List<RealEstateAd>() 
            {
                new RealEstateAd { Id = Guid.NewGuid(), Title = "Home1"},
                new RealEstateAd { Id = Guid.NewGuid(), Title = "Home2"},
                new RealEstateAd { Id = Guid.NewGuid(), Title = "Home3"}
            };

            adService.Setup(a => a.Find( It.IsAny<Expression<Func<RealEstateAd,bool>>>())).Returns(ads.AsQueryable());
            
            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            var fileService = new Mock<UserFileService>();
            var mappingService = new Mock<MappingService>();
            var server = new Mock<HttpServerUtilityBase>();
            var curUserSvc = new Mock<CurrentUserService>();

            var controller = new RealEstateAdController(adService.Object, cityService.Object,
                imgService.Object, fileService.Object, curUserSvc.Object, mappingService.Object, server.Object);
            var view = controller.Index(Guid.NewGuid()) as ViewResult;
            var resultModel = (IEnumerable<RealEstateAd>)view.ViewData.Model;
            Assert.IsNotNull(view);
            Assert.AreEqual(resultModel.Count(), ads.Count(), "Result model has incorrect count");
        }

    }
}
