namespace Reservations.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Models;
    using Services;

    /// <summary>
    /// API controller to manage reservations
    /// </summary>
    public class ReservationsController : ApiController
    {
        private readonly IReservationsService _service;

        public ReservationsController(IReservationsService service)
        {
            _service = service;
        }

        // GET: api/Reservations
        public IEnumerable<ReservationItem> Get()
        {
            return _service.All();
        }

        // GET: api/Reservations/5
        public ReservationItem Get(int id)
        {
            return _service.GetById(id);
        }

        // GET: api/Reservations?day=2015-01-02&hallNumber=202
        public IEnumerable<ReservationItem> Get(DateTime day, int hallNumber)
        {
            return _service.GetByDay(day, hallNumber);
        }

        // GET: api/Reservations?day=2015-01-02
        public IEnumerable<HallFreeHoursStatisticsItem> Get(DateTime day)
        {
            return _service.GetHallsFreeHoursByDay(day);
        }

        // POST: api/Reservations
        // Reservation item must be specified in request body
        public ValidationResult Post([FromBody]NewReservationItem item)
        {
            return _service.Add(item);
        }

        // DELETE: api/Reservations/5
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
