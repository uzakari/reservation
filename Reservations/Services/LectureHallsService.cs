namespace Reservations.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Db;
    using Models;

    /// <summary>
    /// Implementation of business logic methods concerning lecture halls
    /// </summary>
    public class LectureHallsService : ILectureHallsService
    {
        private readonly GetAllLectureHallsQuery _query;

        public LectureHallsService(GetAllLectureHallsQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// Lists all lecture halls that exist in db
        /// </summary>
        public IEnumerable<LectureHallItem> All()
        {
            return Mapper.Map<IEnumerable<LectureHallItem>>(_query.Execute().ToList()); 
        }
    }
}