namespace Reservations.Tests.Services
{
    using System.Linq;

    using FluentAssertions;
    using NUnit.Framework;
    using Reservations.Db;
    using Reservations.Services;

    [TestFixture]
    public class LectureHallsServiceTests
    {
        private GetAllLectureHallsQuery _mockQueryAllLectureHalls;
        private IDatabase _mockDb;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfig.Init();
            _mockDb = TestDatabaseFactory.CreateDatabase();
            _mockQueryAllLectureHalls = new GetAllLectureHallsQuery(_mockDb);
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllLectureHallsRunsAgainstEmptyDatabase_Then_EmptyCollectionIsReturned()
        {
            // Arrange
            var service = new LectureHallsService(new GetAllLectureHallsQuery(TestDatabaseFactory.CreateEmptyDatabase()));

            // Act
            var allLectureHalls = service.All();

            // Assert
            allLectureHalls.Should().BeEmpty();
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllLectureHallsRunsAgainstNonEmptyDatabase_Then_AllLectureHallsAreReturned()
        {
            // Arrange
            var service = new LectureHallsService(_mockQueryAllLectureHalls);

            // Act
            var allLectureHalls = service.All();

            // Assert
            allLectureHalls.Count().Should().Be(8);
        }
    }
}
