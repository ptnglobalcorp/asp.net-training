using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeBlog.Core.Entities;
using AwesomeBlog.Core.Entities.Common;
using AwesomeBlog.Core.Interfaces;
using AwesomeBlog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace AwesomeBlog.Infrastructure.Tests.Persistence
{
    public class EfRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _databaseFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public EfRepositoryTests(DatabaseFixture databaseFixture, ITestOutputHelper testOutputHelper)
        {
            _databaseFixture = databaseFixture;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(DatabaseFixture.TestCategoryIdNotFound)]
        [InlineData(null)]
        public async Task GetByIdAsync_Should_Throw_Exception_When_Not_Found(string id)
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => 
                await _databaseFixture.CategoryRepository
                    .GetByIdAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_An_Entity_When_Found()
        {
            var category = await _databaseFixture.CategoryRepository
                .GetByIdAsync(DatabaseFixture.TestCategoryId1, CancellationToken.None);
            
            Assert.NotNull(category);
            Assert.Equal(DatabaseFixture.TestCategoryId1, category.Id);
            Assert.Equal(DatabaseFixture.TestCategoryName1, category.Name);
        }

        [Fact]
        public async Task ListAllAsync_Should_Return_A_List_Of_Items()
        {
            var categories = await _databaseFixture.CategoryRepository
                .ListAllAsync(CancellationToken.None);
            
            Assert.NotNull(categories);
            Assert.Equal(2, categories.Count);
        }

        [Fact]
        public async Task AddAsync_Should_Create_Successfully_With_Audit_Data()
        {
            const string testCategoryName = "Category Test";
            const string testCategoryDescription = "";

            var category = new Category
            {
                Name = testCategoryName,
                Description = testCategoryDescription
            };

            var created = await _databaseFixture.CategoryRepository
                .AddAsync(category, CancellationToken.None);
            
            Assert.NotNull(created);
            Assert.NotEmpty(created.Id);
            Assert.Equal(testCategoryName, created.Name);
            Assert.Equal(testCategoryDescription, created.Description);
            
            Assert.NotEqual(DateTime.MinValue, created.CreatedAt.DateTime);

            var createdCategory = await _databaseFixture.CategoryRepository
                .GetByIdAsync(created.Id, CancellationToken.None);
            
            Assert.NotNull(createdCategory);
            Assert.Equal(testCategoryName, createdCategory.Name);
            Assert.Equal(testCategoryDescription, createdCategory.Description);
            
            Assert.NotEqual(DateTime.MinValue, createdCategory.CreatedAt.DateTime);
        }
    }

    public class DatabaseFixture : IDisposable
    {
        private const string TestDbConnectionString = "Server=(local);Database=AwesomeBlogTests;User Id=sa;Password=c64;";
        
        public const string TestCategoryId1 = "e45c3dd9-7da3-4bcb-8482-d518214651c4";
        public const string TestCategoryName1 = "Category 1";
        
        public const string TestCategoryId2 = "5ccc97bf-742a-4511-ac89-eaf0f54a8cee";
        public const string TestCategoryName2 = "Category 2";

        public const string TestCategoryIdNotFound = "c1c1b738-9ae5-4c67-b365-8909253b940d";

        public const string TestCurrentUserId = "1a7f604d-64f6-4df6-9c6d-8d9812ba8afe";

        public readonly IRepository<Category> CategoryRepository;
        
        public DatabaseFixture()
        {
            // var options = new DbContextOptionsBuilder<AwesomeBlogDbContext>()
            //     .UseInMemoryDatabase("EfRepositoryTests")
            //     .Options;
            
            var options = new DbContextOptionsBuilder<AwesomeBlogDbContext>()
                .UseSqlServer(TestDbConnectionString)
                .Options;

            var dbContext = new AwesomeBlogDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

            var categories = new List<Category>
            {
                new()
                {
                    Id = TestCategoryId1,
                    Name = TestCategoryName1,
                    Description = "",
                    CreatedAt = DateTimeOffset.UtcNow
                },
                new()
                {
                    Id = TestCategoryId2,
                    Name = TestCategoryName2,
                    Description = "",
                    CreatedAt = DateTimeOffset.UtcNow
                }
            };
            
            dbContext.Categories.AddRange(categories);
            dbContext.SaveChanges();
            
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock
                .Setup(service => service.UserId)
                .Returns(TestCurrentUserId);

            CategoryRepository = new EfRepository<Category>(currentUserServiceMock.Object, dbContext);
        }
        
        public void Dispose()
        {
        }
    }
}