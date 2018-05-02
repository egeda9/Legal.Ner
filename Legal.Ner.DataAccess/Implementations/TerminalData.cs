using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Implementations
{
    public class TerminalData : ITerminalData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);

        public TerminalData()
        {
        }

        public void Insert(List<Terminal> terminals, int fileKeyId, int treeId)
        {
            const string insertTerminal = "INSERT INTO Terminal(Id,Tid,FileKey_Id,Tree_Id) values (@Id,@Tid,@FileKey_Id,@Tree_Id);";
            try
            {
                foreach (Terminal terminal in terminals)
                    _db.Execute(insertTerminal, new { Id = terminal.Id, Tid = terminal.Tid, FileKey_Id = fileKeyId, Tree_Id = treeId });
            }

            catch (Exception ex)
            {
                
            }
        }
    }
}
