using System.Web.Mvc;
using Legal.Ner.Business.Interfaces;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Legal.Ner.DataAccess.Implementations;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Business.Implementations;
using Legal.Ner.Log;
using Legal.Ner.Log.Implementations;

namespace Legal.Ner.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
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
            container.RegisterType<IGraphData, GraphData>();
            container.RegisterType<ISparqlData, SparqlData>();
            container.RegisterType<ISparqlPredifinedNamespacesPrefixesData, SparqlPredifinedNamespacesPrefixesData>();
            container.RegisterType<ILogger, Logger>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}