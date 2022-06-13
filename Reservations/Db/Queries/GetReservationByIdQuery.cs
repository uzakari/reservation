namespace Reservations.Db
{
    /// <summary>
    /// Object representation of query returning reservation by its id from db
    /// </summary>
    public class GetReservationByIdQuery
    {
        private readonly IDatabase _db;

        public GetReservationByIdQuery(IDatabase db)
        {
            _db = db;
        }

        public Reservation Execute(int id)
        {
            return _db.Reservations.ContainsKey(id) ? _db.Reservations[id] : null;
        }
    }
}