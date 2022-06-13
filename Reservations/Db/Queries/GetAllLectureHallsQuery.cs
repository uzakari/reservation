namespace Reservations.Db
{
    using System.Linq;

    /// <summary>
    /// Object representation of query returning all lecture halls from db
    /// </summary>
    public class GetAllLectureHallsQuery
    {
        private readonly IDatabase _db;

        public GetAllLectureHallsQuery(IDatabase db)
        {
            _db = db;
        }

        public IQueryable<LectureHall> Execute()
        {
            return _db.LectureHalls.Values.AsQueryable();
        }
    }
}