using System;
using FluentValidation;
using SimpleInjector;

namespace ConfApp.Web
{
    public class SimpleInjectorValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container _container;

        public SimpleInjectorValidatorFactory(Container container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.GetInstance(validatorType) as IValidator;
        }
    }
}