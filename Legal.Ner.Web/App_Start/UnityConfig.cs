using Legal.Ner.Business.Implementations;
using Legal.Ner.Business.Interfaces;
using Legal.Ner.DataAccess.Implementations;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Log;
using Legal.Ner.Log.Implementations;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Legal.Ner.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. contaz|iner.RegisterType<ITestService, TestService>();
            container.RegisterType<IEntityBulkData, EntityBulkData>();
            container.RegisterType<IEntityData, EntityData>();
            container.RegisterType<ITermData, TermData>();
            container.RegisterType<ITerminalData, TerminalData>();
            container.RegisterType<IFileKeyData, FileKeyData>();
            container.RegisterType<INonTerminalData, NonTerminalData>();
            container.RegisterType<ITreeData, TreeData>();
            container.RegisterType<ITreeEdgeData, TreeEdgeData>();
            container.RegisterType<IWordData, WordData>();
            container.RegisterType<IProcessFile, ProcessFile>();
            container.RegisterType<ISparqlData, SparqlData>();
            container.RegisterType<ISparqlPredifinedNamespacesPrefixesData, SparqlPredifinedNamespacesPrefixesData>();
            container.RegisterType<ILogger, Logger>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}