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
    public class WordData : IWordData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public WordData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public void Insert(List<Word> words, int fileKeyId)
        {
            const string insertEntity = "INSERT INTO Word(Wid,Sent,Para,Offset,Length,Value,FileKey_Id) values (@Wid,@Sent,@Para,@Offset,@Length,@Value,@FileKey_Id);";
            try
            {
                foreach (Word word in words)
                    _db.Execute(insertEntity, new { Wid = word.Wid, Sent = word.Sent, Para = word.Para, Offset = word.Offset, Length = word.Length, Value = word.Value, FileKey_Id = fileKeyId });
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
