using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRecomendationWebApp;
using MovieRecomendationWebApp.Controllers;

namespace MovieRecomendationWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Movie Rating", result.ViewBag.Title);
        }
    }
}
