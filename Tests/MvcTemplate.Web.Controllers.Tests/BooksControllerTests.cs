namespace MvcTemplate.Web.Controllers.Tests
{
    using Moq;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Services.Web;
    using MvcTemplate.Web.ViewModels.Home;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class BooksControllerTests
    {
        [Test]
        public void ByIdShouldWorkCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(BooksController).Assembly);

            const string BookTitle = "SomeContent";
            var booksServiceMock = new Mock<IBookService>();

            var categoryServiceMock = new Mock<ICategoryService>();
            booksServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Book()
                {
                    Title = BookTitle,
                    Category = new BookCategory() { Name = "ffsdads" },
                });

            var controller = new BooksController(booksServiceMock.Object, new UploadBookService(), categoryServiceMock.Object);
            controller.WithCallTo(x => x.ById("fadasasds"))
                .ShouldRenderView("ById")
                .WithModel<BookViewModel>(
                viewModel =>
                {
                    Assert.AreEqual(BookTitle, viewModel.Title);
                })
                .AndNoModelErrors();
        }
    }
}
