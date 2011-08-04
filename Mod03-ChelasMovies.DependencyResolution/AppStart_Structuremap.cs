using System.Web.Mvc;
using StructureMap;

//[assembly: WebActivator.PreApplicationStartMethod(typeof(Mod03_ChelasMovies.DependencyResolution.AppStart_Structuremap), "Start")]

namespace Mod03_ChelasMovies.DependencyResolution {
    public static class AppStart_Structuremap {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}