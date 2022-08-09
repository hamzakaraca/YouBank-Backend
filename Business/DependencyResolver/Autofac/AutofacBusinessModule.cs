using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Security.JWT;
using Core.Utilities.IntegratedValidations.Concrete;
using Core.Utilities.IntegratedValidations.Abstract;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountManager>().As<IAccountService>().SingleInstance();
            builder.RegisterType<EfAccountDal>().As<IAccountDal>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCostumerDal>().As<ICostumerDal>().SingleInstance();

            builder.RegisterType<CreditManager>().As<ICreditService>().SingleInstance();
            builder.RegisterType<EfCreditDal>().As<ICreditDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<KPSPublicSoapClientAdapter>().As<ICheckPersonService>();




            var assembly = System.Reflection.Assembly.GetExecutingAssembly();



            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
