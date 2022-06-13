[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Reservations.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Reservations.NinjectWebCommon), "Stop")]

namespace Reservations
{
    using System;
    using System.Web;
    using System.Web.Http;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using Db;
    using Services;

    /// <summary>
    /// Base configuration of dependency injections
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            // services

            kernel.Bind<ILectureHallsService>().To<LectureHallsService>();
            kernel.Bind<ILecturersService>().To<LecturersService>();
            kernel.Bind<IReservationsService>().To<ReservationsService>();
            kernel.Bind<ISubjectsService>().To<SubjectsService>();

            // queries

            kernel.Bind<GetAllLectureHallsQuery>().ToSelf();
            kernel.Bind<GetAllLecturersQuery>().ToSelf();
            kernel.Bind<GetAllReservationsQuery>().ToSelf();
            kernel.Bind<GetAllSubjectsQuery>().ToSelf();
            kernel.Bind<GetReservationByIdQuery>().ToSelf();
            kernel.Bind<AddReservationQuery>().ToSelf();
            kernel.Bind<DeleteReservationQuery>().ToSelf();

            // db

            kernel.Bind<IDatabase>().ToConstant(DatabaseFactory.CreateDatabase()).InSingletonScope();
        }
    }
}
