using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.ServiceLayer;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace RenewalLetterGenerator
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProcessFile, ProcessFileFacadeService>();
            container.RegisterType<ICSVReader, CSVReaderService>();
            container.RegisterType<ICreateRenewLetter, CreateRenewLetterFacadeService>();
            container.RegisterType<IFileReadAndWriteService, FileReadandWriteService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}