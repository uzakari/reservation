namespace Reservations.Db
{
    using System.Linq;

    /// <summary>
    /// Object representation of query returning all reservations from db
    /// </summary>
    public class GetAllReservationsQuery
    {
        private readonly IDatabase _db;

        public GetAllReservationsQuery(IDatabase db)
        {
            _db = db;
        }

        public IQueryable<Reservation> Execute()
        {
            return _db.Reservations.Values.AsQueryable();
        }
    }
}