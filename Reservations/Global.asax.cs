namespace Reservations
{
    using System.Web.Http;
    using System.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.Init();
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
