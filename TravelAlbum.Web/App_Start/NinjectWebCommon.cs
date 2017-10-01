[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TravelAlbum.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TravelAlbum.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TravelAlbum.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TravelAlbum.Data.Contracts;
    using TravelAlbum.Data;
    using TravelAlbum.Data.EfDbSetWrappers;
    using TravelAlbum.DataServices.Contracts;
    using TravelAlbum.DataServices;
    using System.Data.Entity;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // kernel.Bind<ITravelAlbumDbContextSaveChanges>().To<TravelAlbumEfDbContext>().InRequestScope();

            kernel.Bind<TravelAlbumEfDbContext>().ToSelf().InRequestScope();
            kernel.Bind<ITravelAlbumDbContextSaveChanges>().ToMethod(ctx => ctx.Kernel.Get<TravelAlbumEfDbContext>());
            kernel.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<TravelAlbumEfDbContext>());

            kernel.Bind(typeof(IEfDbSetWrapper<>)).To(typeof(EfDbSetWrapper<>));
            kernel.Bind<ITravelService>().To<TravelService>();
            kernel.Bind<ITravelTranslationalInfoService>().To<TravelTranslationalInfoService>();

      
        }        
    }
}
