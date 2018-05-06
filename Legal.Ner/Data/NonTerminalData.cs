﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using log4net;
using Legal.Ner.DTO;

namespace Legal.Ner.Data
{
    public class NonTerminalData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public NonTerminalData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(NonTerminalData));
        }

        public void Insert(List<NonTerminal> nonTerminals, int fileKeyId, int treeId)
        {
            const string insertNonTerminal = "INSERT INTO NonTerminal(Id,Label,FileKey_Id,Tree_Id) values (@Id,@Label,@FileKey_Id,@Tree_Id);";
            try
            {
                foreach (NonTerminal nonTerminal in nonTerminals)
                    _db.Execute(insertNonTerminal, new { Id = nonTerminal.Id, Label = nonTerminal.Label, FileKey_Id = fileKeyId, Tree_Id = treeId });
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }
    }
}