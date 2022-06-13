namespace Reservations.Db
{
    using System.Linq;

    /// <summary>
    /// Object representation of query returning all subjects from db
    /// </summary>
    public class GetAllSubjectsQuery
    {
        private readonly IDatabase _db;

        public GetAllSubjectsQuery(IDatabase db)
        {
            _db = db;
        }

        public IQueryable<Subject> Execute()
        {
            return _db.Subjects.Values.AsQueryable();
        }
    }
}