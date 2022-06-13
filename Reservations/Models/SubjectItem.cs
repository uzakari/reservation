namespace Reservations.Models
{
    /// <summary>
    /// View model used for presenting subject item (dedicated for external web communication)
    /// </summary>
    public class SubjectItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public string[] Lecturers { get; set; }
    }
}