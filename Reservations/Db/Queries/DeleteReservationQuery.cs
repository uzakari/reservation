namespace Reservations.Db
{
    /// <summary>
    /// Object representation of query deleting reservation entry from db
    /// </summary>
    public class DeleteReservationQuery
    {
        private readonly IDatabase _db;

        public DeleteReservationQuery(IDatabase db)
        {
            _db = db;
        }

        public void Execute(int id)
        {
            if (_db.Reservations.ContainsKey(id))
            {
                _db.Reservations.Remove(id);
            }
        }
    }
}