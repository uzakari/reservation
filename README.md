## Introduction
  
  
The Reservations project is about exposing a University WEB API for the purposes of generating information on lecture halls. There is a given set
of subjects, lecturers and lecture halls. One lecturer is assigned to one subject, one subject can have many lecturers. 
Reservation concerns specific lecturer in a specific lecture hall in some timeframe. API allows listing lecturers, 
subjects and lecture halls - there is no possibility to manipulate them. The project concentrates on reservations - 
we can list/add/delete and get basic statistics about them.
 
Ninject is used to resolve dependencies and query a pattern for data retrieval. A simple, custom, in-memory database is used 
as a datastore for the purposes of this project. Other packages used: AutoMapper (mapping between entities and
view models), NUnit and FluentAssertions for unit tests.

## Problem Statement
  
  
Your job is to implement validation rules used while adding a new reservation. 
**Note:** You have to check all the rules and give a customer full information about the validation results. Do not stop after the first validation failure - that's why enum with `[Flags]` Attribute is returned

Implement following validation rules:
 - `newReservation.From` must be the same day as `newReservation.To`. If it isn't, set result `|= ValidationResult.MoreThanOneDay`
 - `newReservation.From` obviously can't be `>= newReservation.To`. If it is, set result `|= ValidationResult.ToBeforeFrom`
 - whole `newReservation` must be included inside working hours: 8-18 (it can't start before 8 and must finish at 18 at the very latest). If it's not met, set result `|= ValidationResult.OutsideWorkingHours`
 - `newReservation` must last 3 hours at most. If it's not, set result `|= ValidationResult.TooLong`
 - `newReservation` obviously cannot be in conflict (same `hallNumber` and overlapping hours) with any existing reservation. If it is, set result `|= ValidationResult.Conflicting`. Use `_queryAll` to get all extisting reservations
 - check if `newReservation.LectureHallNumber` points at existing lecture hall. If it's not, set result `|= ValidationResult.HallDoesNotExist`. Use `_queryAllLectureHalls` to get all extisting lecture halls
 - check if `newReservation.LecturerId` points at existing lecturer. If it's not, set result `|= ValidationResult.LecturerDoesNotExist`. Use `_queryAllLecturers` to get all extisting lecturers

**Remember!** Check ALL validation rules and set result with appropriate enum flag described above.
Note that for reservation dates, we take into account only date and an hour, minutes and seconds doesn't matter.

## Hints

To **run tests** on your local environment, you may be required to run them as `administrator` or/and in Visual Studio go to `Tools` | `Options` | `Test` | `General` and uncheck the `For improved performance, only use test adapters in test assembly folders or as specified in runsettings file` checkbox.
