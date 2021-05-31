using System.Web.Mvc;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.IntegrationTests.Base;
using BlogApplication.IntegrationTests.Generators;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.ViewModels.Article;
using BlogApplication.Web.Controllers;
using NUnit.Framework;

namespace BlogApplication.IntegrationTests.Web.Controllers
{
    [TestFixture]
    public class ArticleControllerIntegrationTest : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpDependencyInjection();
            _articleController = GetService<ArticleController>();
            _databaseContext = GetService<IDatabaseContext>();
        }

        private ArticleController _articleController;
        private IDatabaseContext _databaseContext;

        [Test]
        public void Given_model_When_add_Then_return_actionResult()
        {
            // Given
            var model = new ArticleAddViewModel
            {
                Text = {Text = StringGenerator.Generate(100)},
                Title = {Text = StringGenerator.Generate(10)}
            };

            RedirectToRouteResult routeResult;

            // When
            _databaseContext.BeginTransaction();

            try
            {
                var actionResult = _articleController.Add(model).Result;
                routeResult = (RedirectToRouteResult) actionResult;
            }
            finally
            {
                _databaseContext.Rollback();
            }

            // Then
            Assert.NotNull(routeResult);
            Assert.AreEqual(ControllerNames.Article, routeResult.RouteValues[ControllerNames.Controller]);
            Assert.AreEqual(ActionNames.List, routeResult.RouteValues[ActionNames.Action]);
        }
    }
}