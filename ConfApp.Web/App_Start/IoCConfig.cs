using ConfApp.Data;
using ConfApp.Data.Repositories;
using ConfApp.Domain;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Data;

namespace ConfApp.Web
{
    using System.Reflection;
    using System.Web.Mvc;
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

            // Repositories and Data Access Registrations
            container.Register<IContext, ApplicationContext>(Lifestyle.Singleton);
            container.Register<IConferenceRepository, ConferenceRepository>();

            // Model Validations Registrations
            container.Register<IValidator<CreateConference>, CreateConferenceValidator>();
            container.Register<IValidator<EditConference>, EditConferenceValidator>();
            container.Register<IValidatorFactory, SimpleInjectorValidatorFactory>();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }
    }
}