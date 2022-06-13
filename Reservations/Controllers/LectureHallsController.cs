namespace Reservations.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Models;
    using Services;

    /// <summary>
    /// API controller to manage listing lecture halls
    /// </summary>
    public class LectureHallsController : ApiController
    {
        private readonly ILectureHallsService _service;

        public LectureHallsController(ILectureHallsService service)
        {
            _service = service;
        }

        // GET: api/LectureHalls
        public IEnumerable<LectureHallItem> Get()
        {
            return _service.All();
        }
    }
}
