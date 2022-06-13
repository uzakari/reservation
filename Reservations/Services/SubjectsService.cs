namespace Reservations.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Db;
    using Models;

    /// <summary>
    /// Implementation of business logic methods concerning subjects
    /// </summary>
    public class SubjectsService : ISubjectsService
    {
        private readonly GetAllSubjectsQuery _query;

        public SubjectsService(GetAllSubjectsQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// Lists all subjects that exist in db
        /// </summary>
        public IEnumerable<SubjectItem> All()
        {
            return Mapper.Map<IEnumerable<SubjectItem>>(_query.Execute().ToList());
        }
    }
}