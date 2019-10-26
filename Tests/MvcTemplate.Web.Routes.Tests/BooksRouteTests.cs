namespace MvcTemplate.Web.Routes.Tests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using MvcTemplate.Web.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class BooksRouteTests
    {
        [Test]
        public void TestRouteByID()
        {
            var routeCollection = new RouteCollection();
            const string url = "/Book/MTQuMzQyMzM0MjM0";
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(url).To<BooksController>(c => c.ById("MTQuMzQyMzM0MjM0"));
        }
    }
}
