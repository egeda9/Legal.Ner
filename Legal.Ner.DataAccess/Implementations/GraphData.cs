using System;
using System.Collections.Generic;
using System.Configuration;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Neo4jClient;

namespace Legal.Ner.DataAccess.Implementations
{
    public class GraphData : IGraphData
    {
        private readonly GraphClient _graphClient;

        public GraphData()
        {
            _graphClient = new GraphClient(new Uri(ConfigurationManager.AppSettings["Neo4JUri"]), ConfigurationManager.AppSettings["Neo4JUser"], ConfigurationManager.AppSettings["Neo4JPassword"]);
            _graphClient.Connect();
        }

        public IEnumerable<BaseSemanticGraph> GetItems(string filter)
        {
            var nodes = _graphClient.Cypher
                .Match($"(n:{filter})")
                .Return(n => new {Node = n.As<BaseSemanticGraph>(), NodeLabels = n.Labels(), Id = n.Id()})
                .Results;

            IList<BaseSemanticGraph> semanticGraphs = new List<BaseSemanticGraph>();

            foreach (var node in nodes)
            {
                BaseSemanticGraph semanticGraph = new BaseSemanticGraph
                {
                    Label = node.Node.Label,
                    Uri = node.Node.Uri,
                    Id = node.Id,
                    NodeLabels = string.Join(":", node.NodeLabels)
                };
                semanticGraphs.Add(semanticGraph);
            }
            return semanticGraphs;
        }

        public void CreateNode(BaseSemanticGraph baseSemanticGraph)
        {
            string dependencies = $"(n:{baseSemanticGraph.SourceClasses}";

            _graphClient.Cypher
                .Create(dependencies + "{baseSemanticGraph})")
                .WithParam("baseSemanticGraph", baseSemanticGraph)
                .ExecuteWithoutResults();
        }

        public void RelateNodes(string sourceMatch, string targetMatch, string relation, string sourceFilter, string targetFilter)
        {
            if (string.IsNullOrEmpty(sourceFilter) || string.IsNullOrEmpty(targetFilter))
                return;

            _graphClient.Cypher
                .Match($"(s:{sourceMatch})", $"(t:{targetMatch})")
                .Where((BaseSemanticGraph nodeSource) => nodeSource.Label == sourceFilter)
                .AndWhere((BaseSemanticGraph nodeTarget) => nodeTarget.Label == targetFilter)
                .Create($"s-[:{relation}]->t")
                .ExecuteWithoutResults();
        }

        public void DeleteNode(BaseSemanticGraph baseSemanticGraph)
        {
            _graphClient.Cypher
                .Match($"(s:{baseSemanticGraph.SourceClasses})")
                .Where((BaseSemanticGraph s) => s.Label == baseSemanticGraph.Label)
                .Delete("s")
                .ExecuteWithoutResults();
        }

        public void UpdateUriNode(BaseSemanticGraph baseSemanticGraph)
        {
            _graphClient.Cypher
                .Match($"(s:{baseSemanticGraph.SourceClasses})")
                .Where((BaseSemanticGraph s) => s.Label == baseSemanticGraph.Filter)
                .Set("s = {graph}")
                .WithParam("graph", baseSemanticGraph)
                .ExecuteWithoutResults();
        }
    }
}
