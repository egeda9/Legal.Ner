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
    public class WordData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public WordData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(WordData));
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
                _log.Error(ex.Message);
            }
        }
    }
}
