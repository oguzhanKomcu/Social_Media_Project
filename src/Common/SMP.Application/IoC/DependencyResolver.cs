using Autofac;
using AutoMapper;
using FluentValidation;
using SMP.Application.AutoMapper;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;
using SMP.Application.Services.FavoritePostService;
using SMP.Application.Services.FollowService;
using SMP.Application.Services.HashtagService;
using SMP.Application.Services.PageService;
using SMP.Application.Services.PostCommentService;
using SMP.Application.Services.PostScoreService;
using SMP.Application.Services.PostService;
using SMP.Application.Services.PostSharingService;
using SMP.Application.Validation.FluentValidation;
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



            base.Load(builder);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            




            #region Services

            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<FavoritePostService>().As<IFavoritePostService>().InstancePerLifetimeScope();
            builder.RegisterType<FollowService>().As<IFollowService>().InstancePerLifetimeScope();
            builder.RegisterType<HashtagService>().As<IHashtagService>().InstancePerLifetimeScope();
            builder.RegisterType<PageService>().As<IPageService>().InstancePerLifetimeScope();
            builder.RegisterType<PostCommentService>().As<IPostCommentService>().InstancePerLifetimeScope();
            builder.RegisterType<PostScoreService>().As<IPostScoreService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<PostSharingService>().As<IPostSharingService>().InstancePerLifetimeScope();


            #endregion


            #region Fluent Validation

            builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();

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


        }
    }
}
