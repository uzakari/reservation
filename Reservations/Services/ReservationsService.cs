namespace Reservations.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Db;
    using Models;

    /// <summary>
    /// Implementation of business logic methods concerning reservations
    /// </summary>
    public class ReservationsService : IReservationsService
    {
        private readonly GetAllReservationsQuery _queryAll;
        private readonly GetReservationByIdQuery _queryById;
        private readonly AddReservationQuery _queryAdd;
        private readonly DeleteReservationQuery _queryDelete;

        private readonly GetAllLecturersQuery _queryAllLecturers;
        private readonly GetAllLectureHallsQuery _queryAllLectureHalls;

        public ReservationsService(GetAllReservationsQuery queryAll, GetReservationByIdQuery queryById, AddReservationQuery queryAdd, DeleteReservationQuery queryDelete, GetAllLecturersQuery queryAllLecturers, GetAllLectureHallsQuery queryAllLectureHalls)
        {
            _queryAll = queryAll;
            _queryById = queryById;
            _queryAdd = queryAdd;
            _queryDelete = queryDelete;
            _queryAllLecturers = queryAllLecturers;
            _queryAllLectureHalls = queryAllLectureHalls;
        }

        /// <summary>
        /// Lists all reservations that exist in db
        /// </summary>
        public IEnumerable<ReservationItem> All()
        {
            return Mapper.Map<IEnumerable<ReservationItem>>(_queryAll.Execute().ToList());
        }

        /// <summary>
        /// Gets single reservation by its id
        /// </summary>
        public ReservationItem GetById(int id)
        {
            return Mapper.Map<ReservationItem>(_queryById.Execute(id));
        }

        /// <summary>
        /// Checks whether given reservation can be added.
        /// Performs logical and business validation.
        /// </summary>
        public ValidationResult ValidateNewReservation(NewReservationItem newReservation)
        {
            if (newReservation == null)
            {
                throw new ArgumentNullException("newReservation");
            }

            var result = ValidationResult.Default;


            // TODO
            // Implement following validation rules:
            // (idea is to check all and give customer full information, not to stop after first fail - therefore enum with [Flags] Attribute is returned)
            // - newReservation.From must be the same day as newReservation.To. If it isn't, set result |= ValidationResult.MoreThanOneDay
            // - newReservation.From obviously can't be >= newReservation.To. If it is, set result |= ValidationResult.ToBeforeFrom
            // - whole newReservation must be included inside working hours: 8-18 (it can't start before 8 and must finish at 18 at the very latest).
            //   If it's not met, set result |= ValidationResult.OutsideWorkingHours
            // - newReservation must last 3 hours at most. If it's not, set result |= ValidationResult.TooLong
            // - newReservation obviously cannot be in conflict (same hallNumber and overlapping hours) with any existing reservation.
            //   If it is, set result |= ValidationResult.Conflicting. Use _queryAll to get all extisting reservations
            // - check if newReservation.LectureHallNumber points at existing lecture hall. If it's not, 
            //   set result |= ValidationResult.HallDoesNotExist. Use _queryAllLectureHalls to get all extisting lecture halls
            // - check if newReservation.LecturerId points at existing lecturer. If it's not, 
            //   set result |= ValidationResult.LecturerDoesNotExist. Use _queryAllLecturers to get all extisting lecturers

            // Remember ! Check ALL validation rules and set result with appropriate enum flag described above.
            // Note that for reservation dates, we take into account only date and an hour, minutes and seconds don't matter.

            return result;
        }

        /// <summary>
        /// Tries to add given reservation to db, after validating it
        /// </summary>
        public ValidationResult Add(NewReservationItem newReservation)
        {
            if (newReservation == null)
            {
                throw new ArgumentNullException("newReservation");
            }

            var result = ValidateNewReservation(newReservation);
            if ((result & ValidationResult.Ok) == ValidationResult.Ok)
            {
                var reservation = new Reservation
                {
                    From = newReservation.From,
                    To = newReservation.To,
                    Lecturer = _queryAllLecturers.Execute().Single(p => p.Id == newReservation.LecturerId),
                    Hall = _queryAllLectureHalls.Execute().Single(p => p.Number == newReservation.LectureHallNumber),
                };

                _queryAdd.Execute(reservation);
            }

            return result;
        }

        /// <summary>
        /// Deletes (if exists) reservation from db (by its id)
        /// </summary>
        public void Delete(int id)
        {
            _queryDelete.Execute(id);
        }

        /// <summary>
        /// Returns all reservations (listed chronologically) on a given day concerning given hall. 
        /// If a given lecture hall does not exist, throws exception
        /// </summary>
        public IEnumerable<ReservationItem> GetByDay(DateTime day, int hallNumber)
        {
            if (!_queryAllLectureHalls.Execute().Any(p => p.Number == hallNumber))
            {
                throw new ArgumentException("Given hall does not exist");
            }

            var reservations =
                _queryAll.Execute().Where(p => p.Hall.Number == hallNumber).Where(p => p.From.Date == day.Date).OrderBy(p => p.From.Hour);

            return Mapper.Map<IEnumerable<ReservationItem>>(reservations.ToList());
        }

        /// <summary>
        /// Returns statistics (list of pairs [HallNumber, NumberOfFreeHours]) on a given day.
        /// Maximum number of free hours is 10 (working hours are 8-18), minimum 0 of course.
        /// Given day must be from the future (not in the past or today).
        /// </summary>
        public IEnumerable<HallFreeHoursStatisticsItem> GetHallsFreeHoursByDay(DateTime day)
        {
            if (day.Date <= DateTime.Today.Date)
            {
                throw new ArgumentException("Input argument must be a future day");
            }

            var result = new List<HallFreeHoursStatisticsItem>();

            var occupiedHallsStatistics =
                _queryAll.Execute().Where(p => p.From.Date == day.Date).GroupBy(p => p.Hall.Number).Select(p => new HallFreeHoursStatisticsItem
                {
                    HallNumber = p.Key,
                    FreeHoursNumber = p.Sum(r => r.To.Hour - r.From.Hour)
                });

            result.AddRange(_queryAllLectureHalls.Execute().Select(p => new HallFreeHoursStatisticsItem
            {
                HallNumber = p.Number,
                FreeHoursNumber = 10 - (occupiedHallsStatistics.Any(r => r.HallNumber == p.Number) ? occupiedHallsStatistics.Single(r => r.HallNumber == p.Number).FreeHoursNumber : 0)
            }));

            return result;
        }
    }
}