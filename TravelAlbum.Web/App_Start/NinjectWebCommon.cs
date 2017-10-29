[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TravelAlbum.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TravelAlbum.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TravelAlbum.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TravelAlbum.Data.EfDbSetWrappers;
    using System.Data.Entity;
    using TravelAlbum.Data;
    using TravelAlbum.DataServices.Contracts;
    using TravelAlbum.DataServices;
    using TravelAlbum.Data.Contracts;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();


        public static IKernel Kernel
        {
            get;
            private set;
        }

        public static Action<IKernel> DependenciesRegistration = kernel =>
        {
            // kernel.Bind<ITeleimotDbContext>().To<TeleimotDbContext>();
            // kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<TravelAlbumEfDbContext>().ToSelf().InRequestScope();
            kernel.Bind<ITravelAlbumEfDbContextSaveChanges>().To<TravelAlbumEfDbContextSaveChanges>();
            kernel.Bind(typeof(IEfDbSetWrapper<>)).To(typeof(EfDbSetWrapper<>));
            
        };


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
            Kernel = new StandardKernel();
            try
            {
                Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(Kernel);
                return Kernel;
            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DependenciesRegistration(kernel);
            kernel.Bind<ITravelService>().To<TravelService>();
            kernel.Bind<ITravelTranslationalInfoService>().To<TravelTranslationalInfoService>();
            kernel.Bind<ITravelImageService>().To<TravelImageService>();
            kernel.Bind<ISingleImageService>().To<SingleImageService>();
            kernel.Bind<ISingleImageTranslationalInfoService>().To<SingleImageTranslationalInfoService>();
        }        
    }
}
