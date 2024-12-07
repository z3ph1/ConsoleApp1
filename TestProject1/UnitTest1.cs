using ConsoleApp1;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Should_Rollback_When_Exception_Occurs()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .Options;

            var mockContext = new Mock<MyDbContext>(options);
            var mockSet = new Mock<DbSet<Entity>>();

            mockContext.Setup(m => m.Entities).Returns(mockSet.Object);

            mockContext.Setup(m => m.BeginTransactionAsync())
                       .Returns(Task.CompletedTask)
                       .Verifiable();

            mockContext.Setup(m => m.RollbackTransactionAsync())
                       .Returns(Task.CompletedTask)
                       .Verifiable();

            var service = new MyService(mockContext.Object);

            // Act
            // Här testar vi att när DoWorkThatFailsAsync kastar undantag,
            // ska RollbackTransactionAsync anropas.
            await Assert.ThrowsAsync<Exception>(service.DoWorkThatFailsAsync);

            // Assert
            mockContext.Verify(m => m.BeginTransactionAsync(), Times.Once);
            mockContext.Verify(m => m.RollbackTransactionAsync(), Times.Once);
            mockContext.Verify(m => m.CommitTransactionAsync(), Times.Never);
        }
    }
}