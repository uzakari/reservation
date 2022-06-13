namespace Reservations.Services
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for business logic methods concerning lecture halls
    /// </summary>
    public interface ILectureHallsService
    {
        IEnumerable<LectureHallItem> All();
    }
}
