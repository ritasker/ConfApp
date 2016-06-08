using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;

namespace ConfApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
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
