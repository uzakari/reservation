namespace Reservations.Db
{
    using System.Linq;

    /// <summary>
    /// Object representation of query returning all lecturers from db
    /// </summary>
    public class GetAllLecturersQuery
    {
        private readonly IDatabase _db;

        public GetAllLecturersQuery(IDatabase db)
        {
            _db = db;
        }

        public IQueryable<Lecturer> Execute()
        {
            return _db.Lecturers.Values.AsQueryable();
        }
    }
}