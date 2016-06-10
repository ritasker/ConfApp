namespace ConfApp.Web
{
    using System.Reflection;
    using System.Web.Mvc;
    using Data;
    using Domain;
    using FluentValidation;
    using Models.Conferences;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public static class IoCConfig
    {
        public static Container Configure()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IContext, ApplicationContext>(Lifestyle.Scoped);

            container.Register<IValidator<CreateConference>, CreateConferenceValidator>();
            container.Register<IValidatorFactory, SimpleInjectorValidatorFactory>();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }
    }
}