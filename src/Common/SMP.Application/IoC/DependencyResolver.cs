using Autofac;
using AutoMapper;
using SMP.Domain.UoW;
using SMP.Infrastructure.UoW;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.IoC
{
    public class DependencyResolver : Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
       
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            




            #region Services

          //  builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            

            #endregion


            #region Fluent Validation

            //builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            //builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();

            #endregion


            #region AutoMapper

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            #endregion


            base.Load(builder);
        }
    }
}
