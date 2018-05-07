using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace Legal.Ner.Test
{
    [TestClass]
    public class SparqlQueryTest
    {
        private FusekiConnector _connector;
        private const string Endpoint = "http://localhost:3030/ontologia-legal-colombia/data";

        [TestInitialize]
        public void Initialize()
        {
            _connector = new FusekiConnector(Endpoint);
        }

        [TestMethod]
        public void GetLegalDocumentsTest()
        {
            // Given
            string query = "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                           "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                           "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                           "PREFIX olc: <http://www.semanticweb.org/ontologia-legal-colombia#> " +
                           "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                           "SELECT ?documento WHERE { ?documento rdf:type olc:ACTO_LEGISLATIVO } ";

            // When
            var result = (SparqlResultSet)_connector.Query(query);

            // Then
            Assert.IsFalse(result.IsEmpty);
        }

        [TestMethod]
        public void GetArticlesLegalDocumentByFilterTest()
        {
            // Given
            string query = "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                            "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                            "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                            "PREFIX olc: <http://www.semanticweb.org/ontologia-legal-colombia#> " +
                            "PREFIX skos: <http://www.w3.org/2004/02/skos/core#> " +
                            "SELECT (STR(?prefLabel) AS ?label)(STR(?tema) AS ?topic) " +
                            "WHERE { " +
                            "?titulo olc:ES_TITULO_DE olc:LEY_1581_DE_2012. " +
                            "?articulo olc:ES_ARTICULO_DE ?titulo . " +
                            "OPTIONAL {?articulo skos:prefLabel ?prefLabel} " +
                            "OPTIONAL {?articulo olc:TIENE_TEMA ?tema} " +
                            "FILTER regex(str(?tema), \"datos\") " +
                            "}";

            // When
            var result = (SparqlResultSet)_connector.Query(query);

            // Then
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetArticlesByFilterTest()
        {
            // Given
            string query = "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                           "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                           "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                           "PREFIX olc: <http://www.semanticweb.org/ontologia-legal-colombia#> " +
                           "PREFIX skos: <http://www.w3.org/2004/02/skos/core#> " +
                           "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                           "SELECT ?X ?Y ?Z " +
                           "WHERE { " +
                           "?X rdf:type olc:DOCUMENTO_LEGAL . " +
                           "?Y rdf:type olc:TITULO . " +
                           "?Z rdf:type olc:ARTICULO . " +
                           "?X olc:TIENE_TITULO ?Y . " +
                           "?Y olc:TIENE_ARTICULO ?Z . " +
                           "?Z olc:TIENE_TEMA \"Datos sensibles\"" +
                           "}";

            // When
            var result = (SparqlResultSet)_connector.Query(query);

            // Then
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void RemoteQueryTest()
        {
            // Given
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));
            string query = "DESCRIBE ?person WHERE {?person a <http://dbpedia.org/ontology/Person>} LIMIT 1";

            // When
            IGraph graph = endpoint.QueryWithResultGraph(query);

            // Then
            Assert.IsNotNull(graph);
        }
    }
}
