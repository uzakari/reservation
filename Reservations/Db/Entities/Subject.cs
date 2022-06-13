namespace Reservations.Db
{
    using System.Collections.Generic;

    /// <summary>
    /// Database entity for subject
    /// </summary>
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}