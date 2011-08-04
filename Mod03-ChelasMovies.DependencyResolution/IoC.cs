using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesImpl;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.RepImpl;
using StructureMap;

namespace Mod03_ChelasMovies.DependencyResolution 
{
    public static class IoC 
    {
        public static IContainer Initialize() 
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>{
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            // IMoviesService, ICommentsService
                            x.For<IMoviesService>().HttpContextScoped().Use<RepositoryMoviesService>();
                            x.For<ICommentsService>().HttpContextScoped().Use<RepositoryCommentsService>();

                            // IMoviesRepository
                            x.For<IMoviesRepository>().HttpContextScoped().Use<EFIMDBAPIMoviesRepository>();
                            x.For<ICommentsRepository>().HttpContextScoped().Use<EFCommentsRepository>();

                            // MovieDbContext
                            x.For<MovieDbContext>().HttpContextScoped().Use<MovieDbContext>();
                        });
            return ObjectFactory.Container;
        }
    }
}