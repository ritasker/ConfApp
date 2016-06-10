namespace ConfApp.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using FluentValidation;
    using FluentValidation.Mvc;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IoCConfig.Configure();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentValidationModelValidatorProvider.Configure(provider =>
                provider.ValidatorFactory = container.GetInstance<IValidatorFactory>());
        }
    }
}