namespace Reservations.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Models;
    using Services;

    /// <summary>
    /// API controller to manage listing lecturers
    /// </summary>
    public class LecturersController : ApiController
    {
        private readonly ILecturersService _service;

        public LecturersController(ILecturersService service)
        {
            _service = service;
        }

        // GET: api/Lecturers
        public IEnumerable<LecturerItem> Get()
        {
            return _service.All();
        }
    }
}
