namespace Reservations.Tests.Services
{
    using System;
    using System.Linq;

    using FluentAssertions;
    using NUnit.Framework;

    using Reservations.Db;
    using Reservations.Models;
    using Reservations.Services;

    [TestFixture]
    public class ReservationsServiceTests
    {
        private GetAllLectureHallsQuery _mockQueryAllLectureHalls;
        private GetAllLecturersQuery _mockQueryAllLectures;
        private GetAllReservationsQuery _mockQueryAllReservations;
        private AddReservationQuery _mockQueryAddReservation;
        private DeleteReservationQuery _mockQueryDeleteReservation;
        private GetReservationByIdQuery _mockQueryGetReservationById;

        private IDatabase _mockDb;

        [OneTimeSetUp]
        public void Init()
        {
            AutoMapperConfig.Init();
        }

        private ReservationsService CreateReservationsService(bool empty)
        {
            _mockDb = empty ? TestDatabaseFactory.CreateEmptyDatabase() : TestDatabaseFactory.CreateDatabase();

            _mockQueryAllLectureHalls = new GetAllLectureHallsQuery(_mockDb);
            _mockQueryAllLectures = new GetAllLecturersQuery(_mockDb);
            _mockQueryAllReservations = new GetAllReservationsQuery(_mockDb);
            _mockQueryAddReservation = new AddReservationQuery(_mockDb);
            _mockQueryDeleteReservation = new DeleteReservationQuery(_mockDb);
            _mockQueryGetReservationById = new GetReservationByIdQuery(_mockDb);

            return new ReservationsService(_mockQueryAllReservations, _mockQueryGetReservationById,
                _mockQueryAddReservation, _mockQueryDeleteReservation, _mockQueryAllLectures, _mockQueryAllLectureHalls);
        }

        private ReservationsService CreateReservationsServiceForStatisticsTests()
        {
            _mockDb = TestDatabaseFactory.CreateDatabaseForStatistics();

            _mockQueryAllLectureHalls = new GetAllLectureHallsQuery(_mockDb);
            _mockQueryAllLectures = new GetAllLecturersQuery(_mockDb);
            _mockQueryAllReservations = new GetAllReservationsQuery(_mockDb);
            _mockQueryAddReservation = new AddReservationQuery(_mockDb);
            _mockQueryDeleteReservation = new DeleteReservationQuery(_mockDb);
            _mockQueryGetReservationById = new GetReservationByIdQuery(_mockDb);

            return new ReservationsService(_mockQueryAllReservations, _mockQueryGetReservationById,
                _mockQueryAddReservation, _mockQueryDeleteReservation, _mockQueryAllLectures, _mockQueryAllLectureHalls);
        }

        [Test]
        [Category("Task1")]
        public void When_AddReservationRunsAndItIsCorrect_Then_ItIsAdded()
        {
            // Arrange
            var service = CreateReservationsService(false);

            var newReservation = new NewReservationItem
            {
                From = new DateTime(2015, 1, 2, 14, 0, 0),
                To = new DateTime(2015, 1, 2, 16, 0, 0),
                LectureHallNumber = 202,
                LecturerId = 8
            };

            // Act
            var result = service.Add(newReservation);

            // Assert
            ((result & ValidationResult.Conflicting) == ValidationResult.Conflicting).Should().BeFalse();
            ((result & ValidationResult.Ok) == ValidationResult.Ok).Should().BeTrue();
            service.All().Count().Should().Be(4);
            service.GetById(4).LectureHallNumber.Should().Be(202);
        }

        [Test]
        [Category("Task1")]
        public void
            When_AddReservationRunsAndItIsNoCorrectBecauseOfManyReasons_Then_ItIsNotAddedAndFullInformationIsReturned()
        {
            // Arrange
            var service = CreateReservationsService(false);

            var newReservation = new NewReservationItem
            {
                From = new DateTime(2015, 1, 2, 10, 0, 0),
                To = new DateTime(2015, 1, 2, 20, 0, 0),
                LectureHallNumber = 202,
                LecturerId = 100
            };

            // Act
            var result = service.Add(newReservation);

            // Assert
            ((result & ValidationResult.TooLong) == ValidationResult.TooLong).Should().BeTrue();
            ((result & ValidationResult.OutsideWorkingHours) == ValidationResult.OutsideWorkingHours).Should().BeTrue();
            ((result & ValidationResult.Conflicting) == ValidationResult.Conflicting).Should().BeTrue();
            ((result & ValidationResult.LecturerDoesNotExist) == ValidationResult.LecturerDoesNotExist).Should()
                .BeTrue();
            ((result & ValidationResult.HallDoesNotExist) == ValidationResult.HallDoesNotExist).Should().BeFalse();
            ((result & ValidationResult.Ok) == ValidationResult.Ok).Should().BeFalse();
            service.All().Count().Should().Be(3);
        }
        [Test]
        [Category("Task1")]
        public void When_AddReservationRunsAndItIsNotInConflictOnHall_Then_ItIsAdded()
        {
            // Arrange
            var service = CreateReservationsService(false);

            var newReservation = new NewReservationItem
            {
                From = new DateTime(2015, 1, 2, 9, 0, 0),
                To = new DateTime(2015, 1, 2, 12, 0, 0),
                LectureHallNumber = 201,
                LecturerId = 1
            };

            // Act
            var result = service.Add(newReservation);

            // Assert
            ((result & ValidationResult.Ok) == ValidationResult.Ok).Should().BeTrue();
            service.All().Count().Should().Be(4);
            service.GetById(4).LectureHallNumber.Should().Be(201);
        }


        [Test]
        [Category("Task1")]
        public void When_AddReservationRunsAndItIsNotInConflictWithAnyButIsBorderline_Then_ItIsAdded()
        {
            // Arrange
            var service = CreateReservationsService(false);

            var newReservation = new NewReservationItem
            {
                From = new DateTime(2015, 1, 2, 10, 0, 0),
                To = new DateTime(2015, 1, 2, 11, 0, 0),
                LectureHallNumber = 202,
                LecturerId = 8
            };

            // Act
            var result = service.Add(newReservation);

            // Assert
            ((result & ValidationResult.Conflicting) == ValidationResult.Conflicting).Should().BeFalse();
            ((result & ValidationResult.Ok) == ValidationResult.Ok).Should().BeTrue();
            service.All().Count().Should().Be(4);
            service.GetById(4).LectureHallNumber.Should().Be(202);
        }


        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllReservationsRunsAgainstEmptyDatabase_Then_EmptyCollectionIsReturned()
        {
            // Arrange
            var service = CreateReservationsService(true);

            // Act
            var allReservations = service.All();

            // Assert
            allReservations.Should().BeEmpty();
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_AllReservationsRunsAgainstNotEmptyDatabase_Then_AllReservationsAreReturned()
        {
            // Arrange
            var service = CreateReservationsService(false);

            // Act
            var allReservations = service.All();

            // Assert
            allReservations.Count().Should().Be(3);
        }
        [Test]
        [Category("TestOnExistingCode")]
        public void When_DeleteReservationRunsAndThatReservationDoesNotExist_Then_NothingHappens()
        {
            // Arrange
            var service = CreateReservationsService(true);

            // Act
            service.Delete(1);

            // Assert
            service.All().Should().BeEmpty();
        }
        [Test]
        [Category("TestOnExistingCode")]
        public void When_DeleteReservationRunsAndThatReservationExists_Then_ItIsDeleted()
        {
            // Arrange
            var service = CreateReservationsService(false);

            // Act
            service.Delete(1);

            // Assert
            service.All().Count().Should().Be(2);
            service.GetById(1).Should().BeNull();
            service.GetById(2).Should().NotBeNull();
        }
        [Test]
        [Category("TestOnExistingCode")]
        public void When_GetReservationByIdRunsAndThatReservationDoesNotExist_Then_NullObjectIsReturned()
        {
            // Arrange
            var service = CreateReservationsService(true);

            // Act
            var reservationById = service.GetById(101);

            // Assert
            reservationById.Should().BeNull();
        }

        [Test]
        [Category("TestOnExistingCode")]
        public void When_GetReservationByIdRunsAndThatReservationExists_Then_ProperReservationIsReturned()
        {
            // Arrange
            var service = CreateReservationsService(false);

            // Act
            var reservationById = service.GetById(2);

            // Assert
            reservationById.Id.Should().Be(2);
            reservationById.LectureHallNumber.Should().Be(202);
        }
    }

}
