using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Expressions.Article;
using BlogApplication.Domain.Interfaces.Mapping;
using BlogApplication.Domain.Services.Article;
using BlogApplication.Models.Entities.Article;
using Moq;
using NUnit.Framework;

namespace BlogApplication.UnitTests.Domain.Services.Article
{
    [TestFixture]
    public class ArticleServiceUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _articleCommentExpressions = new Mock<IArticleCommentExpressions>(MockBehavior.Strict);
            _articleExpressions = new Mock<IArticleExpressions>(MockBehavior.Strict);
            _modelMapper = new Mock<IModelMapper>(MockBehavior.Strict);
            _articleCommentRepository = new Mock<IRepository<ArticleCommentEntity>>(MockBehavior.Strict);
            _articleRepository = new Mock<IRepository<ArticleEntity>>(MockBehavior.Strict);

            _articleService = new ArticleService(
                _articleCommentExpressions.Object,
                _articleExpressions.Object,
                _modelMapper.Object,
                _articleCommentRepository.Object,
                _articleRepository.Object);
        }

        private ArticleService _articleService;
        private Mock<IArticleCommentExpressions> _articleCommentExpressions;
        private Mock<IArticleExpressions> _articleExpressions;
        private Mock<IModelMapper> _modelMapper;
        private Mock<IRepository<ArticleCommentEntity>> _articleCommentRepository;
        private Mock<IRepository<ArticleEntity>> _articleRepository;

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void Given_count_When_GetLastCommentedArticlesAsync_Then_return_empty_list(int count)
        {
            // Given
            var query = Enumerable.Empty<ArticleCommentEntity>().AsQueryable();
            Expression<Func<ArticleCommentEntity, DateTime>> byCreationTime = t => DateTime.Now;
            Expression<Func<ArticleCommentEntity, long>> byArticleId = t => 0;
            Expression<Func<IGrouping<long, ArticleCommentEntity>, ArticleCommentEntity>> byFirstOrDefault =
                t => t.FirstOrDefault();
            var comments = new List<ArticleCommentEntity>();
            IEnumerable<ArticleEntity> articles = new List<ArticleEntity>();

            _articleCommentRepository.Setup(t => t.Query()).Returns(query);
            _articleCommentExpressions.Setup(t => t.ByCreationTime()).Returns(byCreationTime);
            _articleCommentExpressions.Setup(t => t.ByArticleId()).Returns(byArticleId);
            _articleCommentExpressions.Setup(t => t.ByFirstOrDefault()).Returns(byFirstOrDefault);
            _articleCommentRepository.Setup(t => t.ToListAsync(query)).ReturnsAsync(comments);
            _modelMapper.Setup(t => t.Map<ArticleCommentEntity, ArticleEntity>(comments)).Returns(articles);

            // When
            var result = _articleService.GetLastCommentedArticlesAsync(count).Result;

            // Then
            Assert.IsEmpty(result);
            Assert.IsInstanceOf<IEnumerable<ArticleEntity>>(result);

            _articleCommentRepository.Verify(t => t.Query(), Times.Once);
            _articleCommentExpressions.Verify(t => t.ByCreationTime(), Times.Once);
            _articleCommentExpressions.Verify(t => t.ByArticleId(), Times.Once);
            _articleCommentExpressions.Verify(t => t.ByFirstOrDefault(), Times.Once);
            _articleCommentRepository.Verify(t => t.ToListAsync(query), Times.Once);
            _modelMapper.Verify(t => t.Map<ArticleCommentEntity, ArticleEntity>(comments), Times.Once);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void Given_count_When_GetTopArticlesAsync_Then_return_empty_list(int count)
        {
            // Given
            var query = Enumerable.Empty<ArticleEntity>().AsQueryable();
            Expression<Func<ArticleEntity, long>> byLikeCount = t => 0;
            var list = new List<ArticleEntity>();

            _articleRepository.Setup(t => t.Query()).Returns(query);
            _articleExpressions.Setup(t => t.ByLikeCount()).Returns(byLikeCount);
            _articleRepository.Setup(t => t.ToListAsync(query)).ReturnsAsync(list);

            // When
            var result = _articleService.GetTopArticlesAsync(count).Result;

            // Then
            Assert.IsEmpty(result);

            _articleRepository.Verify(t => t.Query(), Times.Once);
            _articleExpressions.Verify(t => t.ByLikeCount(), Times.Once);
            _articleRepository.Verify(t => t.ToListAsync(query), Times.Once);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Given_entity_and_isImageDeleted_When_UpdateArticleAsync_Then_return_one(bool isImageDeleted)
        {
            // Given
            var entity = new ArticleEntity();
            Expression<Func<ArticleEntity, bool>> expression = t => true;
            var article = new ArticleEntity();

            _articleExpressions.Setup(t => t.ById(entity.Id, null)).Returns(expression);
            _articleRepository.Setup(t => t.FirstOrDefaultAsync(expression)).ReturnsAsync(article);
            _modelMapper.Setup(t => t.Map(entity, article)).Returns(article);
            _articleRepository.Setup(t => t.Update(article));

            // When
            _articleService.UpdateArticleAsync(entity, isImageDeleted);

            // Then
            _articleExpressions.Verify(t => t.ById(entity.Id, null), Times.Once);
            _articleRepository.Verify(t => t.FirstOrDefaultAsync(expression), Times.Once);
            _modelMapper.Verify(t => t.Map(entity, article), Times.Once);
            _articleRepository.Verify(t => t.Update(article), Times.Once);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void Given_id_When_GetArticleImageAsync_Then_return_empty_list(long id)
        {
            // Given
            Expression<Func<ArticleEntity, bool>> expression = t => true;
            var article = new ArticleEntity();

            _articleExpressions.Setup(t => t.ById(id, null)).Returns(expression);
            _articleRepository.Setup(t => t.FirstOrDefaultAsync(expression)).ReturnsAsync(article);

            // When
            var result = _articleService.GetArticleImageAsync(id).Result;

            // Then
            Assert.IsEmpty(result);

            _articleExpressions.Verify(t => t.ById(id, null), Times.Once);
            _articleRepository.Verify(t => t.FirstOrDefaultAsync(expression), Times.Once);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Given_null_entity_and_isImageDeleted_When_UpdateArticleAsync_Then_return_zero(bool isImageDeleted)
        {
            // Given
            var entity = new ArticleEntity();
            Expression<Func<ArticleEntity, bool>> expression = t => true;
            ArticleEntity article = null;

            _articleExpressions.Setup(t => t.ById(entity.Id, null)).Returns(expression);
            _modelMapper.Setup(t => t.Map(entity, article)).Returns((ArticleEntity) null);
            _articleRepository.Setup(t => t.Update(article));

            // When
            _articleService.UpdateArticleAsync(entity, isImageDeleted);

            // Then
            _articleExpressions.Verify(t => t.ById(entity.Id, null), Times.Once);
            _modelMapper.Verify(t => t.Map(entity, article), Times.Never);
            _articleRepository.Verify(t => t.Update(article), Times.Never);
        }

        [Test]
        [TestCase("", "", 0, 0)]
        [TestCase("", "", int.MinValue, 0)]
        [TestCase("", "", int.MaxValue, 0)]
        [TestCase(null, null, 0, 0)]
        [TestCase(null, null, int.MinValue, 0)]
        [TestCase(null, null, int.MaxValue, 0)]
        [TestCase(" !@#$%^&*()_+ ", "!@#$%^&*()_+", 0, 1)]
        [TestCase("text", "text", 0, 1)]
        public void Given_title_and_count_When_SearchArticlesAsync_Then_return_empty_list(
            string title,
            string expectedTitle,
            int count,
            int callCount)
        {
            // Given
            var split = new List<string> {expectedTitle}.ToArray();
            Expression<Func<ArticleEntity, bool>> byTitle = t => true;
            var query = Enumerable.Empty<ArticleEntity>().AsQueryable();
            var list = new List<ArticleEntity>();

            _articleExpressions.Setup(t => t.ByTitle(split)).Returns(byTitle);
            _articleRepository.Setup(t => t.QueryByExpression(byTitle)).Returns(query);
            _articleRepository.Setup(t => t.ToListAsync(query)).ReturnsAsync(list);

            // When
            var result = _articleService.SearchArticlesAsync(title, count).Result;

            // Then
            Assert.IsEmpty(result);

            _articleExpressions.Verify(t => t.ByTitle(split), Times.Exactly(callCount));
            _articleRepository.Verify(t => t.QueryByExpression(byTitle), Times.Exactly(callCount));
            _articleRepository.Verify(t => t.ToListAsync(query), Times.Exactly(callCount));
        }
    }
}