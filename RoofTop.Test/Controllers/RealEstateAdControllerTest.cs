using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        
        [TestMethod]
        public void Create_Controller_Redirects_To_Index_When_Model_Is_Valid()
        {
            
            var fakeDbContext = new FakeDbContext();
            var x = fakeDbContext.Cities.Where(c => c.Id == 1).Select(c =>c);
            //var adService = new RealEstateAdService(fakeDbContext);
            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            adService.Setup(a => a.Add(It.IsAny<RealEstateAd>())).Returns(1);

            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new RealEstateImageService(fakeDbContext);
            var fileService = new Mock<UserFileService>();
            var postedImg = new Mock<HttpPostedFileBase>();
            fileService.Setup(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).
                Returns(It.IsAny<string>);
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns(It.IsAny<string>());

            var fakeCurUserSvc = new FakeCurrentUser { UserID = "4848bf70-6744-4d31-8434-dee144520982" };

            var controller = new RealEstateAdController(adService.Object, cityService.Object, imgService, fileService.Object, fakeCurUserSvc,server.Object);


            var view = controller.Create(new CreateAdViewModel()) as RedirectToRouteResult;
            
            Assert.IsNotNull(view, "Failed");
        }

        [TestMethod]
        public void Create_Controller_Returns_View_To_Index_When_Model_Is_Not_Valid()
        {

            var fakeDbContext = new FakeDbContext();
            var x = fakeDbContext.Cities.Where(c => c.Id == 1).Select(c => c);
            //var adService = new RealEstateAdService(fakeDbContext);
            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            adService.Setup(a => a.Add(It.IsAny<RealEstateAd>())).Returns(1);

            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new RealEstateImageService(fakeDbContext);
            var fileService = new Mock<UserFileService>();
            var postedImg = new Mock<HttpPostedFileBase>();
            fileService.Setup(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).
                Returns(It.IsAny<string>);
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns(It.IsAny<string>());

            var fakeCurUserSvc = new FakeCurrentUser { UserID = "4848bf70-6744-4d31-8434-dee144520982" };

            var controller = new RealEstateAdController(adService.Object, cityService.Object, imgService, fileService.Object, fakeCurUserSvc, server.Object);

            var ad = new CreateAdViewModel()
            {
                Title = "The New Mansion"
            };

            //Simulate validation otherwise ModelState.IsValid will always be true
            controller.ModelState.AddModelError("Error", "Title is incorrect");
            var view = controller.Create(ad) as ViewResult;
            Assert.AreEqual(view.Model, ad, "Failed");
        }

        [TestMethod]
        public void Create_Controller_Returns_View_To_Index_When_Model_Is_Valid2()
        {

            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            adService.Setup(a => a.Add(It.IsAny<RealEstateAd>())).Returns(1);

            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());

            var imgService = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            imgService.Setup(i=>i.Add(It.IsAny<Image>())).Returns(It.IsAny<int>());

            var fileService = new Mock<UserFileService>();
            fileService.Setup(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(It.IsAny<string>);

            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s=>s.MapPath(It.IsAny<string>())).Returns(It.IsAny<string>());

            var curUserSvc = new Mock<CurrentUserService>();

            var controller = new RealEstateAdController(adService.Object, cityService.Object,
                imgService.Object, fileService.Object, curUserSvc.Object, server.Object);

            var postedImg = new Mock<HttpPostedFileBase>();
            var ad = new CreateAdViewModel() { Title = "The New Mansion", PostedImage = postedImg.Object };
            
            var view = controller.Create(ad) as RedirectToRouteResult;

            adService.Verify(a => a.Add(It.IsAny<RealEstateAd>()), "Failed to call Add method of adService");
            fileService.Verify(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()) , "Failed to call UploadFile");
            Assert.IsNotNull(view, "Controller does not redirect");
        }

        [TestMethod]
        public void Create_Controller_Returns_View_To_Index_When_Model_Is_Not_Valid2()
        {

            var adService = new Mock<RealEstateAdService>(It.IsAny<IApplicationDbContext>());
            var cityService = new Mock<CityService>(It.IsAny<IApplicationDbContext>());
            var imgService = new Mock<RealEstateImageService>(It.IsAny<IApplicationDbContext>());
            var fileService = new Mock<UserFileService>();
            var server = new Mock<HttpServerUtilityBase>();
            var curUserSvc = new Mock<CurrentUserService>();
            
            var controller = new RealEstateAdController(adService.Object, cityService.Object,
                imgService.Object, fileService.Object, curUserSvc.Object, server.Object);

            var ad = new CreateAdViewModel() { Title = "The New Mansion" };

            //Simulate validation otherwise ModelState.IsValid will always be true
            controller.ModelState.AddModelError("Error", "Title is incorrect");
            var view = controller.Create(ad) as ViewResult;
            Assert.AreEqual(view.Model, ad, "Failed");
        }
    }
}
