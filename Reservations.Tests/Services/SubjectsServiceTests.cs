namespace Reservations.Tests.Services
{
    using System.Linq;

    using FluentAssertions;
    using NUnit.Framework;
    using Reservations.Db;
    using Reservations.Services;

    [TestFixture]
    public class SubjectsServiceTests
    {
        private GetAllSubjectsQuery _mockQueryAllSubjects;
        private IDatabase _mockDb;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfig.Init();
            _mockDb = TestDatabaseFactory.CreateDatabase();
            _mockQueryAllSubjects = new GetAllSubjectsQuery(_mockDb);
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllSubjectsRunsAgainstEmptyDatabase_Then_EmptyCollectionIsReturned()
        {
            // Arrange
            var service = new SubjectsService(new GetAllSubjectsQuery(TestDatabaseFactory.CreateEmptyDatabase()));

            // Act
            var allSubjects = service.All();

            // Assert
            allSubjects.Should().BeEmpty();
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllSubjectsRunsAgainstNonEmptyDatabase_Then_AllLecturersAreReturned()
        {
            // Arrange
            var service = new SubjectsService(_mockQueryAllSubjects);

            // Act
            var allSubjects = service.All();

            // Assert
            allSubjects.Count().Should().Be(4);
        }
    }
}