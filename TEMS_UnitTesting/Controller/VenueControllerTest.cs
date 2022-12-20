using AutoFixture;
using Event.Client.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Client.InputModel;

namespace TEMS_UnitTesting.Controller
{
    public class VenueControllerTest : ApiUnitTest<VenueController>
    {
        private Mock<IVenueRepository> mockVenueRepository;
        public override void TestSetup()
        {
            mockVenueRepository = this.CreateAndInjectMock<IVenueRepository>();
            Target = new VenueController(mockVenueRepository.Object);
        }

        public override void TestTearDown()
        {
            mockVenueRepository.VerifyAll();
        }

        [Fact]
        public void GetAllVenueOfSpecificEvent_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.GetAllVenueOfSpecificEvent(0) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }


        [Fact]
        public void GetAllVenueOfSpecificEvent_Ok()
        {
            //Arrange
            var id = Fixture.Create<int>();
            //var venues = Fixture.Create<Venue>();
            //venues.Id = id;
            List<Venue> venues = new List<Venue>(id);
            this.mockVenueRepository.Setup(c => c.GetAllVenueOfSpecificEvent(id)).Returns(venues);

            //Act
            var result = Target.GetAllVenueOfSpecificEvent(id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(venues, result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            this.mockVenueRepository.Verify(m => m.GetAllVenueOfSpecificEvent(id), Times.Once);
        }

        //[Fact]
        //public void GetAllVenueOfSpecificEvent_NotFound()
        //{
        //    //Arrange
        //    int? id1 = null;
        //    int id = (int)id1;
        //    //Act
        //    var result = Target.GetAllVenueOfSpecificEvent(id) as StatusCodeResult;

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        //}

        [Fact]
        public void GetSpecificVenueDetails_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.GetSpecificVenueDetails(0,0) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void GetSpecificVenueDetails_Ok()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var id1 = Fixture.Create<int>();

            List<Venue> venues = new List<Venue>(id);
            this.mockVenueRepository.Setup(c => c.GetSpecificVenueDetails(id,id1)).Returns(venues);

            //Act
            var result = Target.GetSpecificVenueDetails(id,id1) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(venues, result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            this.mockVenueRepository.Verify(m => m.GetSpecificVenueDetails(id,id1), Times.Once);
        }

        [Fact]
        public void GetVenueByCityId_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.GetVenueByCityId(0) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void GetVenueByCityId_Ok()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var id1 = Fixture.Create<int>();

            List<Venue> venues = new List<Venue>(id);
            this.mockVenueRepository.Setup(c => c.GetVenueByCityId(id)).Returns(venues);

            //Act
            var result = Target.GetVenueByCityId(id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(venues, result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            this.mockVenueRepository.Verify(m => m.GetVenueByCityId(id), Times.Once);
        }

        [Fact]
        public void Post_Ok()
        {
            var venue = Fixture.Create<VenueInputModel>();
            var environment = Fixture.Create<IWebHostEnvironment>();

            // this.mockVenueRepository.Setup(c => c.AddVenue(venue));

            //Act
            var result = Target.Post(venue,environment) as StatusCodeResult;

            //Assert
            //Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            //mockVenueRepository.Verify(c => c.AddVenue(venue), Times.Once);
        }


        [Fact]
        public void Post_BadRequest()
        {
            //Arrange

            //Act
            var result = Target.Post(null, null) as StatusCodeResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
