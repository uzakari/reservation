namespace Reservations
{
    using System.Linq;

    using AutoMapper;
    using Db;
    using Models;

    /// <summary>
    /// Configuration of mappings performed by AutoMapper
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.CreateMap<LectureHall, LectureHallItem>();

            Mapper.CreateMap<Lecturer, LecturerItem>()
                .ForMember(li => li.Subject, m => m.MapFrom(src => src.ConductedLecture.Name));

            Mapper.CreateMap<Subject, SubjectItem>()
                .ForMember(si => si.Lecturers,
                    m =>
                        m.MapFrom(
                            src =>
                                src.Lecturers.Select(
                                    p => string.Format("{0}{1} {2}", p.Title, p.Name, p.Surname)).ToArray()));

            Mapper.CreateMap<Reservation, ReservationItem>()
                .ForMember(ri => ri.LectureHallNumber, m => m.MapFrom(src => src.Hall.Number))
                .ForMember(ri => ri.Lecturer,
                    m =>
                        m.MapFrom(
                            src =>
                                string.Format("{0}{1} {2}", src.Lecturer.Title, src.Lecturer.Name, src.Lecturer.Surname)))
                .ForMember(ri => ri.Subject, m => m.MapFrom(src => src.Lecturer.ConductedLecture.Name));
        }
    }
}