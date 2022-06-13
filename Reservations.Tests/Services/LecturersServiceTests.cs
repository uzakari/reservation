namespace Reservations.Tests.Services
{
    using System.Linq;

    using FluentAssertions;
    using NUnit.Framework;
    using Reservations.Db;
    using Reservations.Services;

    [TestFixture]
    public class LecturersServiceTests
    {
        private GetAllLecturersQuery _mockQueryAllLecturers;
        private IDatabase _mockDb;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfig.Init();
            _mockDb = TestDatabaseFactory.CreateDatabase();
            _mockQueryAllLecturers = new GetAllLecturersQuery(_mockDb);
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllLecturersRunsAgainstEmptyDatabase_Then_EmptyCollectionIsReturned()
        {
            // Arrange
            var service = new LecturersService(new GetAllLecturersQuery(TestDatabaseFactory.CreateEmptyDatabase()));

            // Act
            var allLecturers = service.All();

            // Assert
            allLecturers.Should().BeEmpty();
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllLecturersRunsAgainstNonEmptyDatabase_Then_AllLecturersAreReturned()
        {
            // Arrange
            var service = new LecturersService(_mockQueryAllLecturers);
            
            // Act
            var allLecturers = service.All();

            // Assert
            allLecturers.Count().Should().Be(8);
        }
    }
}
