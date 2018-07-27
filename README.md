# Legal.Ner

Project developed as part of the master's degree in Information and Communications Sciences, entitled: Representation of semantic model in the legal domain applied to computer law in Colombia of the Universidad Distrital Francisco José de Caldas - Bogotá, Colombia.

# Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

* [Git](https://git-scm.com/) -  version control system 
* [Visual Studio 2017](https://visualstudio.microsoft.com/vs/) - Integrated development environment (IDE)
* [SQL SERVER 2017 Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express) - microsoft database
* [Apache jena fuseki](https://jena.apache.org/documentation/serving_data/) - a SPARQL server

### Installing

A step by step series of examples that tell you how to get a development env running

```
RESTORE DATABASE Legal.Ner FROM DISK = 'C:\Legal.Ner.BAK'
```
```
git clone https://github.com/egeda9/Legal.Ner.git
```

using apache jena fuseki -- run windows console

```
fuseki-server.bat
```

```
point the connection string to your restored db and the jena key setting (FusekiBaseUri) in web.config to localhost:3030
```

## Running the tests

Run the tests using Visual Studio Test suite

### Break down into end to end tests

Tests were built with the aim to verify results on the inference engine using SPARQL. Project: Legal.Ner.Test

Example:

```
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
```

## Deployment

Add additional notes about how to deploy this on a live system

```
jena fuseki folder contains the configuration settings in: ...\run\configuration. Be sure to change the setting: tdb:location in the ttl file to your local database folder.
```

## Built With

* [.Net framework](https://www.microsoft.com/net) -  version 4.6.1
* [Visual Studio 2017](https://visualstudio.microsoft.com/vs/) - Integrated development environment (IDE)
* [SQL SERVER 2017 Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express) - microsoft database
* [Apache jena fuseki](https://jena.apache.org/documentation/serving_data/) - a SPARQL server
* [Protegé](https://protege.stanford.edu/) - free, open-source ontology editor 

## Versioning

We use [GitHub](https://github.com) for versioning. For the versions available, see the [tags on this repository](https://github.com/egeda9/Legal.Ner/tags).

## Authors

* **Juan Fernando Rojas Moreno** - *Initial work* - [egeda9](https://github.com/egeda9)

## License

License of use and distribution according to the academic intellectual property license of the Universidad Distrital Francisco José de Caldas
