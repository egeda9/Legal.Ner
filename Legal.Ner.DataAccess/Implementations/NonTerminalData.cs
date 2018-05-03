using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using Neo4j.Driver.V1;

namespace Legal.Ner.DataAccess.Implementations
{
    public class NonTerminalData :INonTerminalData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public NonTerminalData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public void Insert(List<NonTerminal> nonTerminals, int fileKeyId, int treeId)
        {
            const string insertNonTerminal = "INSERT INTO NonTerminal(Id,Label,FileKey_Id,Tree_Id) values (@Id,@Label,@FileKey_Id,@Tree_Id);";
            try
            {
                foreach (NonTerminal nonTerminal in nonTerminals)
                    _db.Execute(insertNonTerminal,
                        new {Id = nonTerminal.Id, Label = nonTerminal.Label, FileKey_Id = fileKeyId, Tree_Id = treeId});
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
