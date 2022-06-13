namespace Reservations.Models
{
    /// <summary>
    /// View model used for presenting statistics of free hours by hall number (dedicated for external web communication)
    /// </summary>
    public class HallFreeHoursStatisticsItem
    {
        public int HallNumber { get; set; }
        public int FreeHoursNumber { get; set; }
    }
}