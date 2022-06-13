namespace Reservations.Services
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for business logic methods concerning lecturers
    /// </summary>
    public interface ILecturersService
    {
        IEnumerable<LecturerItem> All();
    }
}
