﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilites.Interceptors;
using Core.Utilites.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();

            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();

            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<ImageManager>().As<IImageService>().SingleInstance();
            builder.RegisterType<EfImageDal>().As<IImageDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<EfCreditCartDal>().As<ICreditCartDal>().SingleInstance();
            builder.RegisterType<CreditCartManager>().As<ICreditCartService>().SingleInstance();

            builder.RegisterType<EfUserOperationClamDal>().As<IUserOperationClaimDal>().SingleInstance();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();

            builder.RegisterType<EfUserPictureDal>().As<IUserPictureDal>().SingleInstance();
            builder.RegisterType<UserPictureManager>().As<IUserPictureService>().SingleInstance();

            builder.RegisterType<EfRefreshTokenDal>().As<IRefreshTokenDal>().SingleInstance();

            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>().SingleInstance();
            builder.RegisterType<FavoriteManager>().As<IFavoriteService>().SingleInstance();

            builder.RegisterType<EfCarRateDal>().As<ICarRateDal>().SingleInstance();
            builder.RegisterType<CarRateManager>().As<ICarRateService>().SingleInstance();

            builder.RegisterType<EfUserRateDal>().As<IUserRateDal>().SingleInstance();
            builder.RegisterType<UserRateManager>().As<IUserRateService>().SingleInstance();

            builder.RegisterType<EfUserCommentDal>().As<IUserCommentDal>().SingleInstance();
            builder.RegisterType<UserCommentManager>().As<IUserCommentService>().SingleInstance();

            builder.RegisterType<EfCarCommentDal>().As<ICarCommentDal>().SingleInstance();
            builder.RegisterType<CarCommentManager>().As<ICarCommentService>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}