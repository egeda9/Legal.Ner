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
                    Comment = node.Node.Comment,
                    Namespace = node.Node.Namespace,
                    Classes = string.Join(":", node.NodeLabels)
                };
                semanticGraphs.Add(semanticGraph);
            }
            return semanticGraphs;
        }

        public void CreateNode(BaseSemanticGraph baseSemanticGraph)
        {
            string dependencies = $"(n:{baseSemanticGraph.Namespace}";

            _graphClient.Cypher
                .Create(dependencies + "{baseSemanticGraph})")
                .WithParam("baseSemanticGraph", baseSemanticGraph)
                .ExecuteWithoutResults();
        }

        public void RelateNodes(string sourceMatch, string targetMatch, string relation, string sourceFilters, string targetFilters)
        {
            if (string.IsNullOrEmpty(sourceFilters) || string.IsNullOrEmpty(targetFilters))
                return;

            string relationship = relation.Split('#')[1];
            string sourceFilter = sourceFilters.Split('$')[0];
            string targetFilter = targetFilters.Split('$')[0];

            _graphClient.Cypher
                .Match($"(s:{sourceMatch})", $"(t:{targetMatch})")
                .Where((BaseSemanticGraph s) => s.Label == sourceFilter)
                .AndWhere((BaseSemanticGraph t) => t.Label == targetFilter)
                .Create($"(s)-[:{relationship}]->(t)")
                .ExecuteWithoutResults();
        }

        public void DeleteNode(BaseSemanticGraph baseSemanticGraph)
        {
            _graphClient.Cypher
                .Match($"(s:{baseSemanticGraph.Namespace})")
                .Where((BaseSemanticGraph s) => s.Label == baseSemanticGraph.Label)
                .Delete("s")
                .ExecuteWithoutResults();
        }

        public void UpdateUriNode(BaseSemanticGraph baseSemanticGraph)
        {
            _graphClient.Cypher
                .Match($"(s:{baseSemanticGraph.Namespace})")
                .Where((BaseSemanticGraph s) => s.Label == baseSemanticGraph.Filter)
                .Set("s = {graph}")
                .WithParam("graph", baseSemanticGraph)
                .ExecuteWithoutResults();
        }
    }
}
