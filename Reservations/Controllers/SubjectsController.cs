namespace Reservations.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Models;
    using Services;

    /// <summary>
    /// API controller to manage listing subjects
    /// </summary>
    public class SubjectsController : ApiController
    {
        private readonly ISubjectsService _service;

        public SubjectsController(ISubjectsService service)
        {
            _service = service;
        }

        // GET: api/Subjects
        public IEnumerable<SubjectItem> Get()
        {
            return _service.All();
        }
    }
}
