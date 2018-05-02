using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using log4net;
using Legal.Ner.DTO;

namespace Legal.Ner.Data
{
    public class TerminalData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public TerminalData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(TerminalData));
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
                _log.Error(ex.Message);
            }
        }
    }
}
