using Moq;
using TEMS.Business.Interface;
using TEMS.Client.Controllers;

namespace TEMS_UnitTesting.Controller
{
    public class TalkDetailsControllerTest : ApiUnitTest<TalksDetailsController>
    {
        private Mock<ITalkDetailsRepository> mockTalkDetailsRepository;
        public override void TestSetup()
        {
            mockTalkDetailsRepository = this.CreateAndInjectMock<ITalkDetailsRepository>();
            Target = new TalksDetailsController(mockTalkDetailsRepository.Object);
        }

        public override void TestTearDown()
        {

            mockTalkDetailsRepository.VerifyAll();
        }

        //[Fact]
        //public void GetSpeakersTalkDetailsById_NotFound()
        //{
        //    //Arrange

        //    //Act
        //    var result = Target.GetSpeakersTalkDetailsById(0) as StatusCodeResult;

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        //}
    }
}
