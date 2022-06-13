namespace Reservations.Services
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for business logic methods concerning subjects
    /// </summary>
    public interface ISubjectsService
    {
        IEnumerable<SubjectItem> All();
    }
}
