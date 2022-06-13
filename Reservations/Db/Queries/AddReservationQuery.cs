namespace Reservations.Db
{
    using System.Linq;

    /// <summary>
    /// Object representation of query adding new reservation entry to db
    /// </summary>
    public class AddReservationQuery
    {
        private readonly IDatabase _db;

        public AddReservationQuery(IDatabase db)
        {
            _db = db;
        }

        public void Execute(Reservation newReservation)
        {
            var id = _db.Reservations.Keys.Max() + 1;        
            newReservation.Id = id;

            _db.Reservations.Add(id, newReservation);
        }
    }
}