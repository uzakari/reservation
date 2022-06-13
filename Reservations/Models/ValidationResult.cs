namespace Reservations.Models
{
    using System;

    /// <summary>
    /// Enum used for presenting a result of trying to add new reservation item.
    /// ValidationResult.Ok means that reservation has been added successfully, otherwise not, 
    /// and enum entries describe why in a meaningful way to the API client. 
    /// Flags attribute is used, because adding new entry can fail because of multiple reasons.
    /// </summary>
    [Flags]
    public enum ValidationResult
    {
        Default = 0,
        MoreThanOneDay = 1,
        ToBeforeFrom = 2,
        OutsideWorkingHours = 4,
        TooLong = 8,
        Conflicting = 16,
        LecturerDoesNotExist = 32,
        HallDoesNotExist = 64,
        Ok = 128
    }
}