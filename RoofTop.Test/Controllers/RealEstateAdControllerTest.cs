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
        public void CreateControllerReturnsView()
        {
            
            var fakeDbContext = new FakeDbContext();
            var x = fakeDbContext.Cities.Where(c => c.Id == 1).Select(c =>c);
            var adService = new RealEstateAdService(fakeDbContext);
            var cityService = new CityService(fakeDbContext);
            var imgService = new RealEstateImageService(fakeDbContext);
            var fileService = new Mock<UserFileService>();
            fileService.Setup(f => f.UploadFile(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).
                Returns("The File Name");
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns("");

            var fakeCurUserSvc = new FakeCurrentUser { UserID = "4848bf70-6744-4d31-8434-dee144520982" };

            var controller = new RealEstateAdController(adService, cityService, imgService, fileService.Object, fakeCurUserSvc,server.Object);

            var ad = new CreateAdViewModel
            {
                Title = "The Mansion",
                Price = 2000M,
                City_Id = 1,
                RoomCount = 2,
                BathCount = 1,
                Cities = new SelectList(new List<City> { 
                    new City {Id = 1, Name = "Manila"},
                }, "Id", "Name")

            };
            var view = controller.Create(ad) as ViewResult;
            Assert.IsNotNull(view, "Failed");
        }
    }
}
