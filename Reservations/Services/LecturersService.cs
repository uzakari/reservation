namespace Reservations.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Db;
    using Models;

    /// <summary>
    /// Implementation of business logic methods concerning lecturers
    /// </summary>
    public class LecturersService : ILecturersService
    {
        private readonly GetAllLecturersQuery _query;

        public LecturersService(GetAllLecturersQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// Lists all lecturers that exist in db
        /// </summary>
        public IEnumerable<LecturerItem> All()
        {
            return Mapper.Map<IEnumerable<LecturerItem>>(_query.Execute().ToList());
        }
    }
}