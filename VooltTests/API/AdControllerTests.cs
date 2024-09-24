using Xunit;
using Moq;
using VooltAPI.Application.Interfaces;
using VooltAPI.Domain.Entities;
using VooltAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace VooltTests.API
{
    public class AdControllerTests
    {
        private readonly Mock<IAdService> _adServiceMock;
        private readonly AdController _adController;

        public AdControllerTests()
        {
            _adServiceMock = new Mock<IAdService>();
            _adController = new AdController(_adServiceMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenAdIsValid()
        {
            // Arrange
            var ad = new Ad { AdId = 1, AdDescription = "Test Ad" };
            _adServiceMock.Setup(service => service.Create(It.IsAny<Ad>())).Returns(ad);

            // Act
            var result = await _adController.Create(ad);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAd = Assert.IsType<Ad>(okResult.Value);
            Assert.Equal(ad.AdId, returnedAd.AdId);
            Assert.Equal(ad.AdDescription, returnedAd.AdDescription);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenAdIsNull()
        {
            // Act
            var result = await _adController.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            _adServiceMock.Setup(service => service.Create(It.IsAny<Ad>())).Returns((Ad)null);

            // Act
            var result = await _adController.Create(new Ad());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenAdIsValid()
        {
            // Arrange
            var ad = new Ad { AdId = 1, AdDescription = "Updated Ad" };
            _adServiceMock.Setup(service => service.Update(It.IsAny<Ad>())).Returns(ad);

            // Act
            var result = await _adController.Update(ad);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAd = Assert.IsType<Ad>(okResult.Value);
            Assert.Equal(ad.AdId, returnedAd.AdId);
            Assert.Equal(ad.AdDescription, returnedAd.AdDescription);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenAdIsNull()
        {
            // Act
            var result = await _adController.Update(null);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            _adServiceMock.Setup(service => service.Update(It.IsAny<Ad>())).Returns((Ad)null);

            // Act
            var result = await _adController.Update(new Ad());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenAdsExist()
        {
            // Arrange
            var ads = new List<Ad> { new Ad { AdId = 1, AdDescription = "Ad 1" }, new Ad { AdId = 2, AdDescription = "Ad 2" } };
            _adServiceMock.Setup(service => service.GetAll()).Returns(ads);

            // Act
            var result = await _adController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAds = Assert.IsType<List<Ad>>(okResult.Value);
            Assert.Equal(2, returnedAds.Count);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenNoAdsExist()
        {
            // Arrange
            _adServiceMock.Setup(service => service.GetAll()).Returns((List<Ad>)null);

            // Act
            var result = await _adController.GetAll();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
